using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace reservation_managment_lab_project_server
{
    class dishes
    {
       

        public Guid Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string category { get; set; }

        public dishes(string name, int price,string category)
        {
            this.name = name;
            this.price = price;
            this.category = category;
        }

        public override bool Equals(object obj)
        {
            return obj is dishes dishes &&
                   name.Equals(dishes.name) &&
                   category.Equals(dishes.category)&&
                   price == dishes.price;
        }
    }
}
