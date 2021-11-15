using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reservation_managment_lab_project_server
{
    class Worker
    {
        
        
        public Guid Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int accessPriority { get; set; }

        public Worker(string first_name, string last_name, int accessPriority)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.accessPriority = accessPriority;
        }

        public override bool Equals(object obj)
        {
            return obj is Worker worker &&
                   first_name.Equals(worker.first_name) &&
                   last_name.Equals (worker.last_name) &&
                   accessPriority == worker.accessPriority;
        }

        public override string ToString()
        {
            return this.first_name+" "+this.last_name;
        }
    }
}
