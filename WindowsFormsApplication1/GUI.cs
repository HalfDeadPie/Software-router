using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class GUI
    {
        Form mainform;
        ListView tableRoutes;

        //Constructor
        public GUI(Form form, ListView rt)
        {
            mainform = form;
            tableRoutes = rt;
        }

        //Function: Add the line to routing table
        public void addRouteLine(Route line)
        {
            ListViewItem item = new ListViewItem(line.Type.ToString());
            item.SubItems.Add(line.Prefix);
            item.SubItems.Add(line.Mask);
            item.SubItems.Add(line.Device.ToString());
            item.SubItems.Add("via " + line.Nexthop);
            item.SubItems.Add(line.Metric);
            lock (tableRoutes){ mainform.Invoke(new MethodInvoker(delegate() { tableRoutes.Items.Add(item); }));}
        }
    }
}
