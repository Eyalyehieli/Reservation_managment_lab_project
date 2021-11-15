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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;

namespace reservation_managment_lab_project_client
{
    /// <summary>
    /// Interaction logic for tableReservation.xaml
    /// </summary>
    public partial class tableReservation : Window
    {

        NetworkStream streamer;
        byte[] request = new byte[10];
        GetWorkerForTable getWorker;
        public tableReservation(TcpClient socket,int table_number) 
        {
            InitializeComponent();
            streamer = socket.GetStream();
            getWorker = new GetWorkerForTable(streamer);
            getWorker.ShowDialog();
            getWorker.get_selected_worker += value => worker_lbl.Content = value;
            table_num_lbl.Content = "table number " + table_number + " reservation";
            NetWorking.SendRequest(streamer, NetWorking.Requestes.GET_RESERVATION, table_number);
            //read the data
        }
    }
}
