using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace reservation_managment_lab_project_server
{
    class Reservation
    {
   
        public Guid Id { get; set; }
        public int table_number { get; set; }
        public Worker worker { get; set; }
        public bool isFinished { get; set; }

        public Reservation(int table_number, Worker worker, bool isFinished)
        {
            this.table_number = table_number;
            this.worker = worker;
            this.isFinished = isFinished;
        }
    }
}
