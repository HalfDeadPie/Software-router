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
    public class ARP
    {
        static ListView  t;
        public ARP(ListView table)
        {
            t = table;
        }

        //adding ARPlog to ARP GUI table
        public static void addARPline(logARP log)
        {
            ListViewItem lineARP = new ListViewItem(log.accessIP);//adding to GUI table
            lineARP.SubItems.Add(log.accessMAC.ToString());
            lineARP.SubItems.Add(log.accessTTL.ToString());
            t.Items.Add(lineARP);
        }


    }

}
