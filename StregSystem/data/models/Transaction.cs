using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public abstract class Transaction
    {
        public Transaction() { }
        public Transaction(User user, DateTime timeStamp, double amount)
        {
            User = user;
            TimeStamp = timeStamp;
            Amount = amount;
        }

        public int ID { get; set; }
        public int ProductID { get; private set; }
        public User User { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public double Amount { get; private set; }
        public abstract override string ToString();
        public abstract void Execute();
        public abstract void setID();

    }
}

