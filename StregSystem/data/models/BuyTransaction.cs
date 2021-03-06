using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class BuyTransaction : Transaction
    {

        public BuyTransaction(User user, DateTime timeStamp, double amount ,Product product, List<User> users) 
            : base(user, timeStamp, amount, users)
        {
            this.product = product;
            Execute();
        }
        public Product product { get; set; }
        public override void Execute()
        {
            if (User.Balance > Amount || product.CanBeBoughtOnCredit)
            {
                User.setOutBalance(Amount);
                setID();
            }
            else
            {
                throw new InsufficientCreditsException();
            }
        }
        public override User getUserByUsername(string userName, List<User> users)
        {
            foreach(User user in users)
            {
                if (user.UserName == userName) return user;
            }
            throw new IndexOutOfRangeException();
        }
        public override void setID()
        {
            string path = "../../../files/transID.csv";
            string[] id;
            string data = "0";
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    data = parser.ReadLine();
                }
            }
            id = data.Split(';');
            if (id[0] == "0")
            {
                ID = 1;
            }
            else
            {
                int lastID = Int32.Parse(id[0]);
                ID = ++lastID;
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{ID}; TransaktionsID; {Amount.ToString()}; Produkt Købt ;{product.Name}; {TimeStamp}; {this.User.UserName}; {this.User.ID};");
            }
        }
        public override string ToString()
        {
            return ($"Indbetaling af {Amount} af {User.UserName} TransActionID {ID}, {TimeStamp}");
        }
    }
}
