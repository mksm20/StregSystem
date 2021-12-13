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
            List<string> Name = new List<string>{ "Martin", "Mortensen" };
            User user = new User(null,"Martin","Mortensen", "bent21", "martin@gmail.com", 5000);
            user.setIncBalance(150);
            Product product = new Product(7,"Monster",12.5,true,false);
            SeasonalProduct seasonal = new SeasonalProduct(product, "2020-10-12", "2021-05-05");
            BuyTransaction transaction = new BuyTransaction(user, DateTime.Now, product.Price, product);
            
            Console.WriteLine(transaction.ID);
        }
    }
}

// Add collection of products and users hashcodes