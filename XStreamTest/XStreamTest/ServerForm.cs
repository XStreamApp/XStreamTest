using DeviceInformation;
using GlobalFunctions;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XStreamTest
{
    public partial class ServerForm : MetroForm
    {
        mainForm form1 = new mainForm();     
        MetroTile metTile = null;
        UdpClient udpCli = null;
        IPEndPoint iPep = null;
        byte[] data;
        int c = 0;

        BackgroundWorker bWorker = new BackgroundWorker();

        public ServerForm(mainForm formMain)
        {
            InitializeComponent();
            this.Show();
            form1 = formMain;
            bWorker.WorkerReportsProgress = true;
            bWorker.WorkerSupportsCancellation = true;
            bWorker.RunWorkerCompleted += BWorker_RunWorkerCompleted;
            bWorker.ProgressChanged += BWorker_ProgressChanged;
            bWorker.DoWork += BWorker_DoWork;

            MessageBox.Show("Running in Construct");

            udpCli = new UdpClient(8810);
            data = new byte[1024];
            iPep = new IPEndPoint(IPAddress.Any, 8810);

            if (!bWorker.IsBusy)
                bWorker.RunWorkerAsync();
        }

        private void BWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                IPEndPoint iPepBroadcast = new IPEndPoint(IPAddress.Broadcast, 8810);
                data = Encoding.ASCII.GetBytes("DISCOVER_XSTREAM_SERVER");
                udpCli.Send(data, data.Length, iPepBroadcast);
                Console.WriteLine("SEND_DISCOVER_XSTREAM_SERVER");

            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
            try
            {
                e.Cancel = false;
                ThisDevice cliObj = new ThisDevice();
                cliObj.ipEp = iPep;
                udpCli.BeginReceive(new AsyncCallback(AfterReceive), cliObj);

                while (!bWorker.CancellationPending)
                {
                    continue;
                }
                e.Cancel = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AfterReceive(IAsyncResult ar)
        {
            byte[] received;
            string receivedStr;
            ThisDevice clObject = (ThisDevice)ar.AsyncState;

            try
            {
                received = udpCli.EndReceive(ar, ref iPep);
                receivedStr = Encoding.UTF8.GetString(received);
                clObject.ipEp = iPep;
                clObject.data = receivedStr;

                if (receivedStr == "DISCOVER_XSTREAM_CLIENT")
                {
                    Console.WriteLine("RECV_XSTREAM_CLIENT");
                    byte[] response = Encoding.ASCII.GetBytes("DISCOVER_XSTREAM_SERVER");
                    udpCli.Send(response, response.Length, clObject.ipEp);
                    Console.WriteLine("SEND_DISCOVER_XSTREAM_SERVER");
                }
                else if (receivedStr == "ADD_XSTREAM_CLIENT")
                {
                    Console.WriteLine("RECV_ADD_XSTREAM_CLIENT");
                    byte[] response = Encoding.ASCII.GetBytes("ADD_SUCCESS");
                    udpCli.Send(response, response.Length, clObject.ipEp);
                    ConnectedDevices.ManipClientObject(clObject);
                }
                else if (receivedStr == "REMOVE_THIS_DEVICE")
                {
                    Console.WriteLine("RECV_REMOVE_THIS_DEVICE");
                    ConnectedDevices.ManipClientObject(clObject);
                }
                Console.WriteLine("True");


                if (!bWorker.CancellationPending)
                {
                    Console.WriteLine(iPep.Address.ToString());
                    bWorker.ReportProgress(0, clObject);

                    ThisDevice cliObj = new ThisDevice();
                    data = new byte[1024];
                    iPep = new IPEndPoint(IPAddress.Any, 8810);
                    cliObj.ipEp = iPep;
                    udpCli.BeginReceive(new AsyncCallback(AfterReceive), cliObj);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("False");
                MessageBox.Show(e.Message);
            }
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThisDevice cObj = (ThisDevice)e.UserState;

            if (ConnectedDevices.UpdateFlag == 1)
            {
                tilesPanel.Controls.Clear();
                Console.WriteLine("Updating");
                
                foreach (var item in ConnectedDevices.ListClient)
                {
                    metTile = new MetroTile();
                    metTile.Tag = item;
                    metTile.Text = item.ToString();
                    metTile.Click += MetTile_Click;
                    tilesPanel.Controls.Add(metTile);
                }
                ConnectedDevices.UpdateFlag = 0;
            }
        }

        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.Write("\nCancelled");
                MessageBox.Show("Cancelled");
            }
            else if (e.Error != null)
            {
                Console.Write("\n" + e.Error.Message);
            }
            else
            {
                Console.Write("\nCompleted");

            }
        }

        private void MetTile_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(((MetroTile)sender).Text, "Event Triggered", MessageBoxButtons.OK);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backLink_Click(object sender, EventArgs e)
        {
            CloseAll();
            form1.Show();
        }
        private void CloseAll()
        {
            if (bWorker.IsBusy)
            {
                bWorker.CancelAsync();
                ConnectedDevices.ListClient.Clear();
                try
                {
                    IPEndPoint iPepBroadcast = new IPEndPoint(IPAddress.Broadcast, 8810);
                    data = Encoding.ASCII.GetBytes("REMOVE_THIS_DEVICE");
                    udpCli.Send(data, data.Length, iPepBroadcast);
                    Console.WriteLine("SEND_REMOVE_REQUEST");
                }
                catch (Exception excp)
                {
                    MessageBox.Show(excp.Message);
                }
                udpCli.Close();
                this.Dispose();
            }
        }
    }
}
