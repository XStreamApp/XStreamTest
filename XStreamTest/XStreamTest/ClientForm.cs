using DeviceInformation;
using MetroFramework.Forms;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using DeviceInformation;
using GlobalFunctions;
using MetroFramework.Controls;

namespace XStreamTest
{
    public partial class ClientForm : MetroForm
    {
        mainForm formsMain = new mainForm();
        MetroTile metTile = null;
        UdpClient udpCli = null;
        IPEndPoint iPep = null;
        byte[] data;
        int c = 0;

        BackgroundWorker bWorker = new BackgroundWorker();

        public ClientForm(mainForm forms1)
        {
            InitializeComponent();
            this.Show();
            formsMain = forms1; 
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
                data = Encoding.ASCII.GetBytes("DISCOVER_XSTREAM_CLIENT");
                Console.WriteLine("SEND_DISCOVER_XSTREAM_CLIENT");
                udpCli.Send(data, data.Length, iPepBroadcast);
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

                //Console.WriteLine(clObject.Data);
                if (receivedStr == "DISCOVER_XSTREAM_SERVER")
                {
                    Console.WriteLine("RECV_" + receivedStr);
                    data = Encoding.ASCII.GetBytes("DISCOVER_XSTREAM_CLIENT");
                    Console.WriteLine("SEND_DISCOVER_XSTREAM_CLIENT");
                    udpCli.Send(data, data.Length, iPep);
                }
                else if (receivedStr == "ADD_SUCCESS")
                {
                    Console.WriteLine("RECEIVE_ADD_SUCCESS");
                    ConnectedDevices.ManipClientObject(clObject);
                }
                else if (receivedStr == "REMOVE_THIS_DEVICE")
                {
                    Console.WriteLine("RECEIVE_REMOVE_THIS_CLIENT");
                    ConnectedDevices.ManipClientObject(clObject);
                }
                Console.WriteLine("True");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("False");
            }

            if (!bWorker.CancellationPending)
            {
                try
                {
                    Console.WriteLine(iPep.Address.ToString());
                    //AvailableDevices.ManipThisDevice(clObject);
                    bWorker.ReportProgress(0, clObject);

                    ThisDevice cliObj = new ThisDevice();
                    data = new byte[1024];
                    iPep = new IPEndPoint(IPAddress.Any, 8810);
                    cliObj.ipEp = iPep;
                    Console.WriteLine("Beginning receive");
                    udpCli.BeginReceive(new AsyncCallback(AfterReceive), cliObj);
                }
                catch (Exception expc)
                {
                    MessageBox.Show(expc.Message);
                }
            }
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ThisDevice cObj = (ThisDevice)e.UserState;
            if (ConnectedDevices.UpdateFlag == 0 && cObj.flag == 1)
            {
                Console.Write("\n" + cObj.data);
            }
            else if (ConnectedDevices.UpdateFlag == 1)
            {
                //availFlowControl.Controls.Clear();
                MessageBox.Show("Updating");
                foreach (var item in ConnectedDevices.ListClient)
                {
                    metTile = new MetroTile();
                    metTile.Tag = item;
                    metTile.Text = item.ToString();
                    //metTile.Click += MetTile_Click;
                    //addedFlowControl.Controls.Add(metTile);
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
            try
            {
                MetroTile metTile = (MetroTile)sender;
                ThisDevice clObject = (ThisDevice)metTile.Tag;
                AddClient(udpCli, clObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CloseAll()
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
            udpCli = null;
            this.Dispose();
        }

        public void AddClient(UdpClient udpCli, ThisDevice clObject)
        {
            Console.WriteLine("RECV_DISCOVER_XSTREAM_RESPONSE");
            byte[] response = Encoding.ASCII.GetBytes("ADD_XSTREAM_CLIENT");
            udpCli.Send(response, response.Length, clObject.ipEp);
            Console.WriteLine("SEND_ADD_XSTREAM_CLIENT");
        }
    }
}
