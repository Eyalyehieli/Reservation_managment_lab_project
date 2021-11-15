using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace reservation_managment_lab_project_server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int NUMBER_OF_CLIENTS = 3;
       
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        TcpClient[] clients = new TcpClient[NUMBER_OF_CLIENTS];
        NetworkStream[] streams=new NetworkStream[NUMBER_OF_CLIENTS];
        Thread[] threadsClient = new Thread[NUMBER_OF_CLIENTS];
        int client_number = 0;
        DataLayer DBServer;
        public MainWindow()
        {
            InitializeComponent();
            DBServer = new DataLayer("Reservation_Lab_Project");
        }

        public void UpdateTextBlock(TextBlock txb, string text)
        {
            txb.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                 {
                     txb.Text += text;
                 }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server.Start();
            MessageBox.Show("server waiting for clients...");//TODO:change to UI thread using Updae UI function
            //UpdateTextBlock(this.messages_txb, "server waiting for clients...");
            while (client_number< NUMBER_OF_CLIENTS)
            {
                int current_client_number = client_number;//to synchronized the thread
                clients[client_number] = server.AcceptTcpClient();
                //MessageBox.Show("client number "+client_number +" connected");
                streams[client_number] = clients[client_number].GetStream();
                threadsClient[client_number] = new Thread(()=>startClient(current_client_number));
                threadsClient[client_number].Start();
                client_number++;
            }
        }
       


        public ThreadStart startClient(int clientNumber)
        {
            byte[] request;
            while(true)
            {
                request=NetWorking.GetRequest(streams[clientNumber]);
                switch((NetWorking.Requestes)request[0])
                {
                    case NetWorking.Requestes.GET_ALL_WORKERS: Get_all_workers(streams[clientNumber]); break;
                }
            }
        }

        public void Get_all_workers(NetworkStream stream)
        {
            string workerString;
            List<Worker> workers = DBServer.get_all_workers();
            foreach (Worker worker in workers)
            {
                workerString = worker.ToString();
                NetWorking.sentStringOverNetStream(stream, workerString);
            }
        } 
    }
}
