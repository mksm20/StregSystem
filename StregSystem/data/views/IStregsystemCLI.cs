using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StregSystem.data.models;
using static StregSystem.data.views.StregsystemCLI;

namespace StregSystem.data.views
{
    public interface IStregsystemCLI
    {
        IStregsystem stregsystem { get; }
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(Transaction transaction);
        void DisplayUserBuysProduct(List<string> transactions);
        void Close();
        void DisplayInsufficientCash(User user, Product product);
        void DisplayGeneralError(string errorString);
        void DisplayUsers(List<User> users);
        void Start();
        public event CommandParseEventHandler CommandParse;
        void DisplayLowCredit(string lowCredits);
        void PrintProductList();
    }
}
