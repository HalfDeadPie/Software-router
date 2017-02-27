using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PcapDotNet.Core;
using System.Threading;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets;
using System.Collections;
using System.Timers;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Arp;

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
        private List<MacAddress> listMAC;
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
            listMAC = new List<MacAddress>();
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
            foreach (MacAddress actual in listMAC)
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
                addARPlog(packet);
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

            IpV4Datagram ip = packet.Ethernet.IpV4;


            logARP log = new logARP(ip.Destination.ToString(), (MacAddress)packet.Ethernet.Source, timeARP);
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

        private void buttonStart1_Click(object sender, EventArgs e)
        {
            enableARP1 = true;
            if (!textboxIP1.Equals(null))
            {
                IP1 = textboxIP1.Text.ToString();
                MessageBox.Show(IP1.ToString());
            }
        }

        private static void buttonReqARP1_Click(object sender, EventArgs e)
        {
            Packet packet = PacketBuilder.Build(
            DateTime.Now,
            new EthernetLayer
            {
                Source = new MacAddress("00:50:56:a5:64:4d"),
                Destination = new MacAddress("00:50:56:9a:78:c7"),
                EtherType = EthernetType.Arp
            },
            new ArpLayer
            {
                ProtocolType = EthernetType.IpV4,
                Operation = ArpOperation.Request,
                SenderHardwareAddress = new byte[] { 3, 3, 3, 3, 3, 3 }.AsReadOnly(), // 03:03:03:03:03:03.
                SenderProtocolAddress = new byte[] { 1, 2, 3, 4 }.AsReadOnly(), // 1.2.3.4.
                TargetHardwareAddress = new byte[] { 4, 4, 4, 4, 4, 4 }.AsReadOnly(), // 04:04:04:04:04:04.
                TargetProtocolAddress = new byte[] { 11, 22, 33, 44 }.AsReadOnly(), // 11.22.33.44.
            });
        }

    }
}
