
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
        private Boolean enableARP1, enableRIP1;
        private Form opener;

        public GUI gui;
        private RIPv2 rip;
        private Ping ping;

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
            enableRIP1 = false;

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
            ping = new Ping();

            //testing
            textboxIP1.Text = "169.254.212.220";
            textboxMask1.Text = "255.255.0.0";
            textboxARPtarget.Text = "169.254.212.209";
            
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
            if (packet.IsValid)
            {
                //-------------------------MY IP OR MULTICAST
               if (enableARP1 == true)
               {

                        if (packet.DataLink.Kind == DataLinkKind.Ethernet)
                        {
                            //ARP PACKET
                            if (packet.Ethernet.EtherType == EthernetType.Arp)
                            {
                                handlerARP(packet);
                            }
                            //IPV4 PACKET
                            else if (packet.Ethernet.EtherType == EthernetType.IpV4)
                            {
                                //enabled RIPv2
                                if (enableRIP1 == true)
                                {
                                    //receiving RIPv2 packet with special destination IP
                                    if (packet.Ethernet.IpV4.Destination.ToString().Equals("224.0.0.9"))
                                    {
                                        rip.handler(packet);
                                    }
                                }
                                //ICMP
                                if (packet.Ethernet.IpV4.Protocol == IpV4Protocol.InternetControlMessageProtocol)
                                {
                                    ping.handler(packet, dev0);
                                }
                            }
                        }
                    
                }
                //-------------------------ANOTHER IP
                else
                {
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
                addConnected(textboxIP1.Text, textboxMask1.Text, 0);
                Invoke(new MethodInvoker(delegate() { buttonStart1.Text = "Stop"; }));
                Invoke(new MethodInvoker(delegate() { textboxIP1.Enabled = false; }));
                Invoke(new MethodInvoker(delegate() { textboxMask1.Enabled = false; }));
                enableARP1 = true;
            }
            else
            { 
                enableARP1 = false;
                Invoke(new MethodInvoker(delegate() { buttonStart1.Text = "Start"; }));
                Invoke(new MethodInvoker(delegate() { textboxIP1.Enabled = true; }));
                Invoke(new MethodInvoker(delegate() { textboxMask1.Enabled = true; }));
            }
        }

        private void buttonReqARP1_Click(object sender, EventArgs e)
        {
            requestARP();
        }

        private void requestARP()
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
            if (enableRIP1 == false)
            {
                Invoke(new MethodInvoker(delegate() { buttonEnable1.Text = "Stop"; }));
                enableRIP1 = true;
            }
            else
            {
                Invoke(new MethodInvoker(delegate() { buttonEnable1.Text = "Start"; }));
                enableRIP1 = false;
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void buttonPing_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => pinger());
            thread.Start();
        }

        private void pinger()
        {
            Route route = findNexthopRoute(textboxARPtarget.Text);

            logARP log = null;
            if (route.Nexthop.Equals("directly connected"))
            {
                log = lookupARP(textboxARPtarget.Text);
            }
            else
            {
                log = lookupARP(route.Nexthop);
            }

            if (log == null)
            {
                requestARP();
            }

            Thread.Sleep(10);

            if (route.Nexthop.Equals("directly connected"))
            {
                log = lookupARP(textboxARPtarget.Text);
            }
            else
            {
                log = lookupARP(route.Nexthop);
            }



            try
            {
                ping.Request(dev0, myMAC1.ToString(), log.accessMAC, textboxIP1.Text, log.accessIP);
            }
            catch (Exception delay)
            {
            }
        }

        private Route findNexthopRoute(String IP)
        {
            Route best = new Route();
            best.Mask = "0.0.0.0";
            best.Distance = 999;
            foreach (Route actual in routeList)
            {
                if(isInSubnet(IP, actual.Prefix, actual.Mask) && actual.Distance < best.Distance &&  betterMask(best.Mask,actual.Mask)){
                    best = actual;
                }
            }
            if (best.Distance != 999)
            {
                return best;
            }
            else
            {
                return null;
            }
        }

        //Function: Return true if IP is in Prefix's subnet
        private bool isInSubnet(String IP, String prefix, String mask)
        {
            byte[] byteIP = IP.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            byte[] bytePrefix = prefix.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            byte[] byteMask = mask.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            byte[] byteMaskedIP = new byte[byteIP.Length];
            byte[] byteMaskedPrefix = new byte[bytePrefix.Length];

            if (byteIP.Length != byteMask.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            for (int i = 0; i < byteIP.Length; i++)
            {
                byteMaskedIP[i] = (byte)(byteIP[i] & byteMask[i]);
                byteMaskedPrefix[i] = (byte)(bytePrefix[i] & byteMask[i]);
            }
            if (byteMaskedPrefix[0] == byteMaskedIP[0] && byteMaskedPrefix[1] == byteMaskedIP[1] &&  byteMaskedPrefix[2] == byteMaskedIP[2] && byteMaskedPrefix[3] == byteMaskedIP[3]) 
                return true;
            else return false;
        }

        //Function: Return true if mask is higher then mask of the actual best route
        private bool betterMask(String bestMask, String actualMask)
        {
            byte[] byteBestMask = bestMask.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            byte[] byteActualMask = actualMask.Split('.').Select(x => Convert.ToByte(x, 10)).ToArray();
            for (int i = 0; i < byteActualMask.Length; i++)
            {
                if (byteActualMask[i] > byteBestMask[i])
                {
                    return true;
                }
            }
            return false;
        }
        
        //Function: Return the ARP log of current destination mathich nexthop
        private logARP lookupARP(String nexthop){
            foreach (String actual in listMAC)
            {
                logARP log = (logARP)hashARP[actual.GetHashCode()];
                if (log.accessIP.Equals(nexthop))
                {
                    return log;
                }
            }
            return null;
        }

        private void addConnected(String IP, String mask, uint device)
        {
            Route connected = new Route();
            connected.Prefix = IP;
            connected.Mask = mask;
            connected.Nexthop = "directly connected";
            connected.Distance = 0;
            connected.Type = 'C';
            lock (routeList) { routeList.Add(connected); };
            gui.addRouteLine(connected);
        }

    }
}
