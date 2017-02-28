using PcapDotNet.Packets.Ethernet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class logARP
    {
        private String IP;

        public String accessIP
        {
            get { return IP; }
            set { IP = value; }
        }
        private String MAC;

        public String accessMAC
        {
            get { return MAC; }
            set { MAC = value; }
        }
        private int ttl;

        public int accessTTL
        {
            get { return ttl; }
            set { ttl = value; }
        }

        public logARP(String IP, String MAC, int ttl)
        {
            this.IP = IP;
            this.MAC = MAC;
            this.ttl = ttl;
        }

        public int reduce(){
            return this.ttl--;
        }
    }
}
