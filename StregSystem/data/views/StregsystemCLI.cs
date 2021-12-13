using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StregSystem.data.models;

namespace StregSystem.data.views
{
    public class StregsystemCLI : IStregsystemCLI
    {
        public StregsystemCLI(Stregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }
        public Stregsystem stregsystem { get; private set; }
        private bool _isStarted = true;
        private string _command;
        public delegate void CommandParseEventHandler(object source, CommandArgs args);
        public event CommandParseEventHandler CommandParse;
        protected virtual void OnCommandParse(string command)
        {
            if (CommandParse != null) CommandParse(this, new CommandArgs() { Command = command });
        }
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
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.User.UserName} has bought {transaction.product.ToString()} \n");
        }
        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            for (int i = 0; i < count; i++)
            {
                //Console.WriteLine($"{transaction.User.UserName} has bought {products.Products. [transaction.User.transactions[^(i+1)].ProductID]}");
            }
        }
        public void Close()
        {
            stregsystem.UpdateUsers();
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
                    OnCommandParse(_command);
                }
            }
        }
    }
}
