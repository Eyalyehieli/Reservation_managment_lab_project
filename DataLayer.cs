using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using MongoDB.Driver;
using MongoDB.Bson;


namespace reservation_managment_lab_project_server
{
    class DataLayer
    {
        IMongoDatabase database;
        public DataLayer(string db_name)
        {
            var client = new MongoClient();
            database = client.GetDatabase(db_name);
            InitializeDB();
        }

        public void InitializeDB()
        {
            List<Worker> workers = new List<Worker>();
            workers.Add(new Worker("Eyal", "Yehieli", 3));
            workers.Add(new Worker("Ido", "Ben - Yosef", 1));
            workers.Add(new Worker("Lidor", "Elmakayes", 1));
            workers.Add(new Worker("Reut", "Maman", 2));
            InsertListOfRecords("workers", workers);
            List<dishes> dishes = new List<dishes>();
            dishes.Add(new dishes("cigar", 45,"firsts"));
            dishes.Add(new dishes("mushrooms", 40,"firsts"));
            dishes.Add(new dishes("meat bruschettas", 60,"firsts"));
            dishes.Add(new dishes("steak", 90,"main dishes"));
            dishes.Add(new dishes("hamburger", 80, "main dishes"));
            dishes.Add(new dishes("fish & chips", 90, "main dishes"));
            dishes.Add(new dishes("ice-cream", 20,"deserts"));
            dishes.Add(new dishes("chocolate cake", 40, "deserts"));
            dishes.Add(new dishes("cheesecake", 45, "deserts"));
            dishes.Add(new dishes("coke", 12,"drinks"));
            dishes.Add(new dishes("water", 10, "drinks"));
            dishes.Add(new dishes("beer", 15, "drinks"));
            InsertListOfRecords("dishes", dishes);
        }

        public void InsertRecord<T>(string table,T record)
        {
            var collection = database.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public void InsertListOfRecords<T>(string table,List<T> list)
        {
            List<T> existRecords = LoadRecords<T>(table);
            bool contains;
            foreach (T rec in list)
            {
                contains=existRecords.Any(item => item.Equals(rec));
                if(!contains)
                {
                    InsertRecord(table, rec);
                    existRecords = LoadRecords<T>(table);
                }
                
            }
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = database.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }


        public  void get_reservation(int table_number)
        {
          
        }
        public  List<Worker> get_all_workers()
        {
           return LoadRecords<Worker>("workers");
        }
    }
}
