using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class TransactionList
    {
        public TransactionList()
        {
            getTransaction();
            Transactions = new List<BuyTransaction>();
        }
        public List<BuyTransaction> Transactions { get; set; }
        private string _path = "../../../files/Transactions.json";

        public void addTransactions()
        {
            using (StreamWriter w = new StreamWriter(_path))
            {
                string json = "[";
                foreach (Transaction transaction in Transactions)
                {
                    json += System.Text.Json.JsonSerializer.Serialize(transaction);
                    json += ",";
                }
                json.Remove(json[^2], 2);
                json += "]";
                w.Write(json);
            }
        }
        private void getTransaction()
        {
            //try
            //{
            //    using (StreamReader r = new StreamReader(_path))
            //    {
            //        string json = r.ReadToEnd();
            //        Transactions = JsonConvert.DeserializeObject<List<BuyTransaction>>(json);
            //    }
            //}
            //catch (FileNotFoundException)
            //{
            //    return;
            //}
        }
    }

}
