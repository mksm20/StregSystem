using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StregSystem.data.models;

namespace StregSystem.data.views
{
    class StregsystemCLI 
    {
        private Stregsystem stregsystem = new Stregsystem();
        private bool _isStarted = true;
        private string _command;
        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"{username} does not exist \n");
        }
        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"{product} does not exist \n");
        }
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(
                $"username: {user.UserName} \n name: {user.FirstName} {user.LastName} \n" +
                $"balance: {user.Balance} \n email: {user.Email}"
                );
        }
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguments passed: {command}");
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"{adminCommand}: is not a recognized commmand");
        }
        public void DisplayUserBuysProduct(int? count, BuyTransaction transaction)
        {
            if (count == null)
            {
                Console.WriteLine($"{transaction.User.UserName} has bought {transaction.product.ToString()} \n");
            }
        }
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction, ProductList products)
        {
            for (int i = 0; i < count; i++)
            {
                //Console.WriteLine($"{transaction.User.UserName} has bought {products.Products. [transaction.User.transactions[^(i+1)].ProductID]}");
            }
        }
        public void Close()
        {
            Environment.Exit(0);
        }
        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"{user.UserName} insufficient funds to buy product {product.Name} price {product.Price} funds {user.Balance}");
        }
        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"There was an error: {errorString} ");
        }
        public void Start()
        {
            Console.WriteLine("Product List: \n");
            foreach (Product product in stregsystem.ActiveProducts)
            {
                Console.WriteLine(product.ToString());
            }
            while (_isStarted)
            {
                _command = Console.ReadLine();
                if(_command != null)
                {
                    
                }
            }
        }
    }
}
