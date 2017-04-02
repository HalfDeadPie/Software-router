﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PcapDotNet.Base;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Arp;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Gre;
using PcapDotNet.Packets.Http;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.Igmp;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.IpV6;
using PcapDotNet.Packets.Transport;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Timers;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication1
{
    public partial class Main : Form
    {
        private int DEV0;
       
        private IList<LivePacketDevice> allDevices;
        private PacketCommunicator dev0;
        private List<Thread> threadList;//all threads list
        public List<Route> routeList;
        private const int SNAPSHOT = 65536, TIMEOUT = 10, AMOUNT = 0, TIME = 1000, timeARP = 30;
        private MacAddress myMAC1;
        private Hashtable hashARP;
        private List<String> listMAC;
        private System.Timers.Timer timerRefresh, timerARP;
        private Boolean enableARP1;
        private Boolean enabled1;
        private Form opener;

        public GUI gui;
        private RIPv2 rip;

        //*****************************************************************************************************************************************
        //Constructor
        public Main(int first, int second)
        {
            this.DEV0 = first;
            init();
            open();
        }

        //Initialization
        private void init()
        {
            InitializeComponent();
            Show();
            threadList = new List<Thread>();
            routeList = new List<Route>();
            allDevices = LivePacketDevice.AllLocalMachine;
            hashARP = new Hashtable();
            listMAC = new List<String>();
            enableARP1 = false;
            enabled1 = false;

            timerRefresh = new System.Timers.Timer(TIME);
            timerRefresh.AutoReset = true;
            this.timerRefresh.Start();
            timerRefresh.Elapsed += Refresh;

            timerARP = new System.Timers.Timer(TIME);
            timerARP.AutoReset = true;
            this.timerARP.Start();
            timerRefresh.Elapsed += periodARP;

            myMAC1 = PcapDotNet.Core.Extensions.LivePacketDeviceExtensions.GetMacAddress(allDevices[DEV0]);

            gui = new GUI(this, tableRoutes);
            rip = new RIPv2(gui,routeList);
            
        }
        //*****************************************************************************************************************************************
        //opening communicators
        private void open()
        {
            //open communcators
            dev0 = allDevices[DEV0].Open(SNAPSHOT, PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, TIMEOUT);
            threadList.Add(new Thread(Receiving0));
            threadList[0].Start();
        }

        //refresh on timerARP ellapse
        public void Refresh(Object source, ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate() { tableARP.Items.Clear(); }));
            foreach (String actual in listMAC)
            {
                addARPline((logARP)hashARP[actual.GetHashCode()]);
            }
        }

        public void periodARP(Object source, ElapsedEventArgs e)
        {
            for (int i = listMAC.Count - 1; i >= 0; i--)
            {
                logARP log = (logARP)hashARP[listMAC[i].GetHashCode()];
                if (log.accessTTL > 1)
                {
                    log.reduce();
                }
                else
                {
                    lock (hashARP) { hashARP.Remove(listMAC[i].GetHashCode()); }
                    lock (listMAC) { listMAC.RemoveAt(i); };
                }
            }
        }

        //receving function for device 0
        private void Receiving0()
        {
            dev0.ReceivePackets(AMOUNT, PacketHandler0);
        }

        //packet handler for device 0
        public void PacketHandler0(Packet packet)
        {
            while (enableARP1 == true)
            {
                if (packet.DataLink.Kind == DataLinkKind.Ethernet)
                {
                    if (packet.Ethernet.EtherType == EthernetType.Arp)
                    {
                        handlerARP(packet);
                    }
                }
            }
            if (enabled1 == true)
            {
                //receiving RIPv2 packet
                if (packet.Ethernet.IpV4.Destination.ToString().Equals("224.0.0.9"))
                {
                    rip.handler(packet);
                }
            }
                
        }

        //adding ARPlog to ARP GUI table
        private void addARPline(logARP log)
        {
            ListViewItem lineARP = new ListViewItem(log.accessIP);//adding to GUI table
            lineARP.SubItems.Add(log.accessMAC.ToString());
            lineARP.SubItems.Add(log.accessTTL.ToString());
            Invoke(new MethodInvoker(delegate() { tableARP.Items.Add(lineARP); }));
        }

        //adding ARP log
        private void handlerARP(Packet packet)
        {
            byte[] senderMACbyte = packet.Ethernet.Arp.SenderHardwareAddress.ToArray();
            String senderMAC = (BitConverter.ToString(senderMACbyte)).Replace("-", ":");
            byte[] senderIPbyte = packet.Ethernet.Arp.SenderProtocolAddress.ToArray();
            String senderIP = "" + senderIPbyte[0] + "." + senderIPbyte[1] + "." + senderIPbyte[2] + "." + senderIPbyte[3];

            logARP log = new logARP(senderIP, senderMAC, timeARP);
            int key = log.accessMAC.GetHashCode();
            if (hashARP[key] == null)
            {
                addARPline(log);
                hashARP.Add(key, log);
                listMAC.Add(log.accessMAC);
            }
            else
            {
                logARP actual = (logARP)hashARP[key];
                actual.accessIP = senderIP;
                actual.accessMAC = senderMAC;
                actual.accessTTL = timeARP;
            }

            String targetMAC = PcapDotNet.Core.Extensions.LivePacketDeviceExtensions.GetMacAddress(allDevices[DEV0]).ToString();
            byte[] targetMACbyte = targetMAC.Split(':').Select(x => Convert.ToByte(x, 16)).ToArray();
            String targetIP = textboxIP1.Text.ToString();
            byte[] targetIPbyte = targetIP.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
    
            byte[] tempIPbyte = packet.Ethernet.Arp.TargetProtocolAddress.ToArray();
            String tempIP = "" + tempIPbyte[0] + "." + tempIPbyte[1] + "." + tempIPbyte[2] + "." + tempIPbyte[3];

            if (textboxIP1.Text.Equals(tempIP.ToString()))
            {
                if (packet.Ethernet.Arp.Operation.ToString().Equals("Request"))
                {
                    Packet replyPacket = PacketBuilder.Build(
                    DateTime.Now,
                    new EthernetLayer
                    {
                        Source = new MacAddress(targetMAC),
                        Destination = new MacAddress(senderMAC),
                        EtherType = EthernetType.Arp
                    },
                    new ArpLayer
                    {
                        ProtocolType = EthernetType.IpV4,
                        Operation = ArpOperation.Reply,
                        SenderHardwareAddress = targetMACbyte.AsReadOnly(),
                        SenderProtocolAddress = targetIPbyte.AsReadOnly(),
                        TargetHardwareAddress = senderMACbyte.AsReadOnly(),
                        TargetProtocolAddress = senderIPbyte.AsReadOnly(),
                    });
                    dev0.SendPacket(replyPacket);
                }
            }
        }

        //Enable device 0
        private void buttonStart1_Click(object sender, EventArgs e)
        {
            if (enableARP1 == false)
            {
                enableARP1 = true;
                Invoke(new MethodInvoker(delegate() { buttonStart1.Text = "Stop"; }));
            }
            else
            { 
                enableARP1 = false;
                Invoke(new MethodInvoker(delegate() { buttonStart1.Text = "Start"; }));
            }
        }

        private void buttonReqARP1_Click(object sender, EventArgs e)
        {
            String senderMAC = PcapDotNet.Core.Extensions.LivePacketDeviceExtensions.GetMacAddress(allDevices[DEV0]).ToString();
            String senderIP = textboxIP1.Text.ToString();
            String targetIP = textboxARPtarget.Text.ToString();

            byte[] senderMACbyte = senderMAC.Split(':').Select(x => Convert.ToByte(x, 16)).ToArray();
            byte[] senderIPbyte = senderIP.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            byte[] targetIPbyte = targetIP.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();

            Packet packet = PacketBuilder.Build(
            DateTime.Now,
            new EthernetLayer
            {
                Source = new MacAddress(senderMAC),
                Destination = new MacAddress("FF:FF:FF:FF:FF:FF"),
                EtherType = EthernetType.Arp
            },
            new ArpLayer
            {
                ProtocolType = EthernetType.IpV4,
                Operation = ArpOperation.Request,
                SenderHardwareAddress = senderMACbyte.AsReadOnly(),
                SenderProtocolAddress = senderIPbyte.AsReadOnly(),
                TargetHardwareAddress = new byte[] { 0, 0, 0, 0, 0, 0 }.AsReadOnly(),
                TargetProtocolAddress = targetIPbyte.AsReadOnly(),
            });
            dev0.SendPacket(packet);
        }

        private void buttonStatic_Click(object sender, EventArgs e)
        {
            StaticRoute formStatic = new StaticRoute(gui,routeList);
            formStatic.Show();
        }

        private void buttonEnable1_Click(object sender, EventArgs e)
        {
            if (enabled1 == false)
            {
                Invoke(new MethodInvoker(delegate() { buttonEnable1.Text = "Stop"; }));
                enabled1 = true;
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { buttonEnable1.Text = "Start"; }));
                enabled1 = false;
            }
        }
    }
}