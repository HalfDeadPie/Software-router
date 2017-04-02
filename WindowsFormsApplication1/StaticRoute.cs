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
    public partial class StaticRoute : Form
    {
        GUI gui;
        List<Route> r;
        public StaticRoute(GUI g, List<Route> routes)
        {
            InitializeComponent();
            gui = g;
            r = routes;
        }

        private void buttonAddStaticRoute_Click(object sender, EventArgs e)
        {
            //create the new log
            Route stat = new Route();
            stat.Prefix = textboxStaticPrefix.Text;
            stat.Mask = textboxStaticMask.Text;
            stat.Nexthop = textboxStaticNexthop.Text;
            stat.Device = (uint)numericDevice.Value;
            stat.Type = 'S';
            r.Add(stat);

            gui.addRouteLine(stat);
        }

    }
}
