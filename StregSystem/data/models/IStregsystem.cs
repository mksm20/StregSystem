using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StregSystem.data.models.Stregsystem;

namespace StregSystem.data.models
{
    public interface IStregsystem
    {
        TransactionList Transactions { get;  }
        ProductList Products { get;  }
        UserList Users { get; } 
        IEnumerable<Product> ActiveProducts { get; }
        void InitiateStregsystem();
        Transaction AddCreditsToAccount(string username, double amount);
        void BuyProduct(User user, Product product);
        Product GetProductByID(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        List<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        void createNewProduct(List<string> product);
        void OnLowBalance(object source, UserArgs e);
        void UpdateUsers();
        void CreditOnOff(int id);
        void ActivateProduct(int id);
        void DeactivateProduct(int id);
        List<string> GetTransactionForUser(string username, int count);
        void GetActiveProducts();
        public event BalanceLowEventHandler LowBalance;

    }
}
