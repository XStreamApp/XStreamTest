using MetroFramework.Forms;
using System;
using MetroFramework.Controls;
using DeviceInformation;
using GlobalFunctions;
using System.Net;

namespace XStreamTest
{
    public partial class mainForm : MetroForm
    {
        public mainForm()
        {
            int cnt = 0;
            Console.WriteLine(Dns.GetHostName());
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ConnectedDevices.GetCompiled(addr);
                    Console.WriteLine(addr);
                }
                cnt++;
            }
            InitializeComponent();
            //string str = SerializationFunctions.Serialize(new ThisDevice());
            //Console.WriteLine(str);
            cnt++;
        }

        private void serverTile_Click(object sender, EventArgs e)
        {
            ServerForm serForm = new ServerForm(this);
            this.Hide();
        }

        private void clientTile_Click(object sender, EventArgs e)
        {
            ClientForm cliForm = new ClientForm(this);
            this.Hide();
        }
    }
}
