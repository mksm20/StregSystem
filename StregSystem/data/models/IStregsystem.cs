using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StregSystem.data.models.User;

namespace StregSystem.data.models
{
    interface IStregsystem
    {
            UserList Users { get; set; } 
            IEnumerable<Product> ActiveProducts { get; }
            void InitiateStregsystem();
            InsertCashTransaction AddCreditsToAccount(string username, double amount);
            BuyTransaction BuyProduct(User user, Product product);
            Product GetProductByID(int id);
            IEnumerable<Transaction> GetTransactions(User user, int count);
            User GetUsers(Func<User, bool> predicate);
            User GetUserByUsername(string username);
            void OnLowBalance(object source, UserArgs e);
            void GetActiveProducts();
    }
}
