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
        public StregsystemCLI(IStregsystem stregsystem)
        {
            this.stregsystem = stregsystem;
        }
        public IStregsystem stregsystem { get; }
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
        public void DisplayLowCredit(string lowCredit)
        {
            Console.WriteLine(lowCredit);
        }
        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"{product} does not exist \n");
        }
        public void DisplayUserInfo(User user)
        {
            Console.WriteLine(
                $"Username: {user.UserName} \nName: {user.FirstName} {user.LastName} \n" +
                $"Balance: {user.Balance} \nEmail: {user.Email}"
                );
        }
        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguments passed: {command}");
        }
        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"{adminCommand}: is not a recognized command");
        }
        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction.User.UserName} has bought {transaction.product.ToString()} \n");
        }
        public void DisplayUserBuysProduct(List<string> transactionHistory)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i < transactionHistory.Count ; i++)
            {
                Console.WriteLine(transactionHistory[i]);
            }
        }

        public void Close()
        {
            Console.Clear();
            stregsystem.UpdateUsers();
            stregsystem.Products.SaveProducts();
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
        public void PrintProductList()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Product List: \n");
            foreach (Product product in stregsystem.ActiveProducts)
            {
                Console.WriteLine(product.ToString());
            }
            Console.SetCursorPosition(0, 0);
        }
        public void Start()
        {
            PrintProductList();
            while (_isStarted)
            {
                _command = Console.ReadLine();
                if(_command != null)
                {
                    Console.Clear();
                    PrintProductList();
                    OnCommandParse(_command);
                }
            }
        }

        public void DisplayUsers(List<User> users)
        {
            foreach(User user in users)
            {
                Console.WriteLine($"{user.UserName} {user.Balance} {user.Email}");
            }
        }
    }
}
