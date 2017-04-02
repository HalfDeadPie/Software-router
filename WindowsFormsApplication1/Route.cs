using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Route
    {
        public Route()
        {
        }
        //protocol
        char type;
        public char Type
        {
          get { return type; }
          set { type = value; }
        }
        
        //ip
        String prefix;
        public String Prefix
        {
          get { return prefix; }
          set { prefix = value; }
        }
        
        //mask
        String mask;
        public String Mask
        {
          get { return mask; }
          set { mask = value; }
        }

        //nextop
        String nexthop;
        public String Nexthop
        {
          get { return nexthop; }
          set { nexthop = value; }
        }

        //interface
        uint device;
        public uint Device
        {
            get { return device; }
            set { device = value; }
        }
    }
}
