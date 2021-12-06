﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, DateTime timeStamp, double amount) 
            : base(user, timeStamp, amount)
        {
            Execute();
        }
        public override void Execute()
        {
            User.setIncBalance(Amount);
            setID();

        }
        public override void setID()
        {
            string path = "../../../files/transID.csv";
            string[] id;
            string data ="0";
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    data = parser.ReadLine();
                }
            }
            id = data.Split(',');
            Console.WriteLine(id);
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
                sw.WriteLine($"{ID},TransaktionsID, {Amount}kr,{TimeStamp}");
            }
        }
        public override string ToString()
        {
            return ($"Indbetaling af {Amount} af {User.UserName} TransActionID {ID}, {TimeStamp}");
        }
    }
}