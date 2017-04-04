using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.Icmp;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Ping
    {
        ushort seq;
        public Ping()
        {
            seq = 1;
        }

        public void handler(Packet packet, PacketCommunicator device)
        {
            //received ping request
            if (packet.Ethernet.IpV4.Icmp.MessageTypeAndCode.ToString().Equals("Echo"))
            {
                PacketCommunicator dev = device;
                String srcMAC = packet.Ethernet.Destination.ToString();
                String dstMAC = packet.Ethernet.Source.ToString();
                String srcIP = packet.Ethernet.IpV4.Destination.ToString();
                String dstIP = packet.Ethernet.IpV4.Source.ToString();

                EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = new MacAddress(srcMAC),
                    Destination = new MacAddress(dstMAC),
                    EtherType = EthernetType.None, // Will be filled automatically.
                };

                IpV4Layer ipV4Layer =
                    new IpV4Layer
                    {
                        Source = new IpV4Address(srcIP),
                        CurrentDestination = new IpV4Address(dstIP),
                        Fragmentation = IpV4Fragmentation.None,
                        HeaderChecksum = null, // Will be filled automatically.
                        Identification = 123,
                        Options = IpV4Options.None,
                        Protocol = null, // Will be filled automatically.
                        Ttl = 100,
                        TypeOfService = 0,
                    };


                IcmpIdentifiedDatagram icmp = (IcmpIdentifiedDatagram)packet.Ethernet.IpV4.Icmp;
                IcmpEchoReplyLayer icmpLayer =
                    new IcmpEchoReplyLayer
                    {
                        Checksum = null, // Will be filled automatically.
                        SequenceNumber = icmp.SequenceNumber,
                        Identifier = icmp.Identifier, //switchID
                    };

                PacketBuilder builder = new PacketBuilder(ethernetLayer, ipV4Layer, icmpLayer);

                //Packet EchoReplyPacket =  builder.Build(DateTime.Now);
                dev.SendPacket(builder.Build(DateTime.Now));
            }
            //received ping response
            else if (packet.Ethernet.IpV4.Icmp.MessageTypeAndCode.ToString().Equals("EchoReply"))
            {
                MessageBox.Show("Ping Reply!");
            }
        }

        public void Request(PacketCommunicator dev, String srcMAC, String dstMAC, String srcIP, String dstIP)
        {
            EthernetLayer ethernetLayer =
            new EthernetLayer
            {
                Source = new MacAddress(srcMAC),
                Destination = new MacAddress(dstMAC),
                EtherType = EthernetType.None, // Will be filled automatically.
            };
            
            IpV4Layer ipV4Layer =
            new IpV4Layer
            {
                Source = new IpV4Address(srcIP),
                CurrentDestination = new IpV4Address(dstIP),
                Fragmentation = IpV4Fragmentation.None,
                HeaderChecksum = null, // Will be filled automatically.
                Identification = 123,
                Options = IpV4Options.None,
                Protocol = null, // Will be filled automatically.
                Ttl = 100,
                TypeOfService = 0,
             };

            Random rnd = new Random();
            ushort id = (ushort) rnd.Next(1, 65535);
            seq++;
            IcmpEchoLayer icmpLayer =
             new IcmpEchoLayer
             {
                Checksum = null, // Will be filled automatically.
                SequenceNumber = seq,
                Identifier = id
             };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, ipV4Layer, icmpLayer);

            dev.SendPacket(builder.Build(DateTime.Now));
        }
    }
}
