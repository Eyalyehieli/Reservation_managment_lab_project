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
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace reservation_managment_lab_project_client
{
    /// <summary>
    /// Interaction logic for GetWorkerForTable.xaml
    /// </summary>
    public partial class GetWorkerForTable : Window
    {
        NetworkStream stream;
        public event Action<string> get_selected_worker;
        public GetWorkerForTable(NetworkStream stream)
        {
            InitializeComponent();
            this.stream = stream;
            loadWorkersToComboBox();
            if(get_selected_worker!=null)
            {
                get_selected_worker(workers_combo_box.SelectedItem.ToString());
            }
        }
       public void loadWorkersToComboBox()
        {
            string worker_name;;
            int table_number = 20;//there is no table like 20 to get all the workers its static
            NetWorking.SendRequest(stream, NetWorking.Requestes.GET_ALL_WORKERS, table_number);
            do
            {
                worker_name = NetWorking.getStringOverNetStream(stream);
                workers_combo_box.Items.Add(worker_name);
            }
            while (stream.DataAvailable);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
