using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class RIPv2
    {
        
        List<Route> routes;
        GUI gui;
        public RIPv2(GUI g, List<Route> r)
        {
            routes = r;
            gui = g;
        }
        public void handler(Packet packet)
        {
            int ttl = packet.Ethernet.IpV4.Ttl;
            String payload = packet.Ethernet.IpV4.Udp.Payload.ToHexadecimalString();    // RIP payload
            int payloadLength = payload.Length;
            String RipHeader = payload.Substring(0, 8);                                 // nepotrebna RIP hlavicka
            String RIPRoutes = payload.Substring(8, payloadLength - 8);                 // samotne routy v surovom stave, jedna za druhou
            int RIPRoutesLength = RIPRoutes.Length;


            if (ttl > 1)
            {
                int i = 0;
                while ((i + 1) * 40 <= RIPRoutesLength) // prechadza vsetky route zaznamy v pakete
                {
                    String route = RIPRoutes.Substring(i * 40, 40);     // jedna routa
                    // vytiahne jednotlive komponenty samotnej routy
                    String family = route.Substring(0, 4);
                    String tag = route.Substring(4, 4);
                    String IP = route.Substring(8, 8);
                    String mask = route.Substring(16, 8);
                    String next_hop = route.Substring(24, 8);
                    String metric = route.Substring(32, 8);

                    // prevod IP adries z HEX tvaru do DEC (DEC.DEC.DEC.DEC)
                    String ajpi = Convert.ToString(Convert.ToInt32(IP.Substring(0, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(IP.Substring(2, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(IP.Substring(4, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(IP.Substring(6, 2), 16));
                    String next = Convert.ToString(Convert.ToInt32(next_hop.Substring(0, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(next_hop.Substring(2, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(next_hop.Substring(4, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(next_hop.Substring(6, 2), 16));
                    String maska = Convert.ToString(Convert.ToInt32(mask.Substring(0, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(mask.Substring(2, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(mask.Substring(4, 2), 16)) + "." + Convert.ToString(Convert.ToInt32(mask.Substring(6, 2), 16));

                    // metrika z 8 bitov spravi INT
                    int met = 0;
                    Int32.TryParse(metric, out met);


                    //create the new log
                    Route stat = new Route();
                    stat.Prefix = ajpi;
                    stat.Mask = maska;
                    stat.Nexthop = next;
                    stat.Device = 0;
                    stat.Type = 'R';
                    stat.Metric = metric;
                    stat.Distance = 120;
                    routes.Add(stat);

                    //gui.addRouteLine(stat);

                    i++;
                }
            }

        }
     }
    
}
