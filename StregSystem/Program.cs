using System;
using System.Collections.Generic;
using StregSystem.data.models;

namespace StregSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<String> name = new List<string>();
            name.Add("Martin");
            name.Add("Mortensen");
            User user = new User(name, "bent21", "martin@gmail.com");
        }
    }
}
