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
        private List<MacAddress> listMAC;
        private System.Timers.Timer timerRefresh, timerARP;
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
            for (int i = listMAC.Count - 1; i >= 0; i-- )
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
    }
}
