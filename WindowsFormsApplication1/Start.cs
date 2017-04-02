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

namespace WindowsFormsApplication1
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            // Retrieve the device list from the local machine
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

            if (allDevices.Count == 0)
            {
                Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            // Print the list
            for (int i = 0; i < allDevices.Count; i++)
            {
                LivePacketDevice device = allDevices[i];
                listInterfaces1.Items.Add(new ListViewItem(device.Description.ToString()));
                listInterfaces2.Items.Add(new ListViewItem(device.Description.ToString()));
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int first = listInterfaces1.SelectedIndices[0];
            int second = listInterfaces2.SelectedIndices[0];
            var mainform = new Main(first, second);
            mainform.Closed += (s, args) => this.Close();
            mainform.Show();
            this.Hide();
        }
    }
}
