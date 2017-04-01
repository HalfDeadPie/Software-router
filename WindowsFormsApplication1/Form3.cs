using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        private ListView t;//routing table
        private List<Rt> r;
        public Form3(ListView table, List<Rt> routes)
        {
            InitializeComponent();
            t = table;
            r = routes;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddStaticRoute_Click(object sender, EventArgs e)
        {
            //create the new log
            Rt stat = new Rt();
            stat.Prefix = textboxStaticPrefix.Text;
            stat.Mask = textboxStaticMask.Text;
            stat.Nexthop = textboxStaticNexthop.Text;
            stat.Device = (uint)numericDevice.Value;
            stat.Type = 'S';
            r.Add(stat);

            ListViewItem item = new ListViewItem(stat.Type.ToString());
            item.SubItems.Add(stat.Prefix);
            item.SubItems.Add(stat.Mask);
            item.SubItems.Add(stat.Nexthop);
            item.SubItems.Add(stat.Device.ToString());
            Invoke(new MethodInvoker(delegate() { t.Items.Add(item); }));
        }

    }
}
