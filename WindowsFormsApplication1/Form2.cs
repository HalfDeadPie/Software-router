
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

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private int DEV0;
        private IList<LivePacketDevice> allDevices;
        private PacketCommunicator dev0;
        private List<Thread> threadList;//all threads list
        private const int SNAPSHOT = 65536, TIMEOUT = 10, AMOUNT = 0, TIME = 1000, timeARP = 10;
        private MacAddress myMAC1;
        private Hashtable hashARP;
        private String IP1;
        private List<String> listMAC;
        private System.Timers.Timer timerRefresh, timerARP;
        private Boolean enableARP1;
        private Form opener;

        //*****************************************************************************************************************************************
        //Constructor
        public Form2(int first, int second)
        {
            this.DEV0 = first;
            init();
        }

        //Initialization
        private void init()
        {
            InitializeComponent();
            Show();
            threadList = new List<Thread>();
            allDevices = LivePacketDevice.AllLocalMachine;
            hashARP = new Hashtable();
            listMAC = new List<String>();
            enableARP1 = false;
            open();

            timerRefresh = new System.Timers.Timer(TIME);
            timerRefresh.AutoReset = true;
            this.timerRefresh.Start();
            timerRefresh.Elapsed += Refresh;

            timerARP = new System.Timers.Timer(TIME);
            timerARP.AutoReset = true;
            this.timerARP.Start();
            timerRefresh.Elapsed += periodARP;

            myMAC1 = PcapDotNet.Core.Extensions.LivePacketDeviceExtensions.GetMacAddress(allDevices[DEV0]);

            //temporary:
            textboxIP1.Text = ("10.10.10.10");
            textboxARPtarget.Text = ("169.254.252.215");
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
            if (enableARP1 == true)
            {
                if (packet.DataLink.Kind == DataLinkKind.Ethernet)
                {
                    if (packet.Ethernet.EtherType == EthernetType.Arp)
                    {
                        addARPlog(packet);
                    }
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
        private void addARPlog(Packet packet)
        {
            //reply ARP incomming
            if (packet.Ethernet.Arp.Operation.ToString().Equals("Reply"))
            {
                byte[] senderMACbyte = packet.Ethernet.Arp.SenderHardwareAddress.ToArray();
                String senderMAC = (BitConverter.ToString(senderMACbyte)).Replace("-", ":"); ;

                byte[] senderIPbyte = packet.Ethernet.Arp.SenderProtocolAddress.ToArray();
                String senderIP = "" + senderIPbyte[0] + "." + senderIPbyte[1] + "." + senderIPbyte[2] + "." + senderIPbyte[3];

                logARP log = new logARP(senderIP,senderMAC,timeARP);
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
                    actual.accessTTL = timeARP;
                }
            }
            else if (packet.Ethernet.Arp.Operation.ToString().Equals("Request"))
            {
                String targetMAC = PcapDotNet.Core.Extensions.LivePacketDeviceExtensions.GetMacAddress(allDevices[DEV0]).ToString();
                String targetIP = textboxIP1.Text.ToString();
                
                byte[] senderMACbyte = packet.Ethernet.Arp.SenderHardwareAddress.ToArray();
                byte[] senderIPbyte = packet.Ethernet.Arp.SenderProtocolAddress.ToArray();
                byte[] targetMACbyte = targetMAC.Split(':').Select(x => Convert.ToByte(x, 16)).ToArray();
                byte[] targetIPbyte = targetIP.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();

                String senderMAC = (BitConverter.ToString(senderMACbyte)).Replace("-", ":"); ;
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
                    actual.accessTTL = timeARP;
                }

                byte[] tempIPbyte = packet.Ethernet.Arp.TargetProtocolAddress.ToArray();
                String tempIP = "" + tempIPbyte[0] + "." + tempIPbyte[1] + "." + tempIPbyte[2] + "." + tempIPbyte[3];
                if (textboxIP1.Text.Equals(tempIP.ToString()))
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
            enableARP1 = true;
            if (!textboxIP1.Equals(null))
            {
                IP1 = textboxIP1.Text.ToString();
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

    }
}
