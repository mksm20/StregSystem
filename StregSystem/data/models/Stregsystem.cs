using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class Stregsystem : IStregsystem
    {
        public Stregsystem()
        {
            Users = new UserList();
            Products = new ProductList();
            Transactions = new TransactionList();
            InitiateStregsystem();
            GetActiveProducts();
        }

        private string _path = "../../../files/users.Json";
        public UserList Users { get; private set; }
        public ProductList Products { get; private set; }
        public TransactionList Transactions { get; private set; }
        public IEnumerable<Product> ActiveProducts { get; private set; }
        public delegate void BalanceLowEventHandler(object source, UserArgs args);
        public event BalanceLowEventHandler LowBalance;

        protected virtual void OnLowBalanceStreg(User e)
        {
            if (LowBalance != null) LowBalance(this, new UserArgs() { user = e });
        }
        public void createNewProduct(List<string> product)
        {
            int ID = Products.Products[^1].ID;
            ID++;
            Product newProduct = new Product(ID, product[0], double.Parse(product[1]), bool.Parse(product[2]), bool.Parse(product[3]));
            Products.Products.Add(newProduct);
            GetActiveProducts();
        }
        public void UpdateUsers()
        {
            using (StreamWriter w = new StreamWriter(_path))
            {
                string json = "[";
                foreach (User user in Users.users)
                {
                    json += System.Text.Json.JsonSerializer.Serialize(user);
                    json += ",";
                }
                json += "]";
                w.Write(json);
            }

        }

        public List<string> GetTransactionForUser(string username, int count)
        {
            List<string> transactionsForPrint = new List<string>();
            int j = 0;
            using (StreamReader r = new StreamReader("../../../files/transID.csv")) 
            {
                
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
                    string[] tok = line.Split(";");
                    if (tok[6].Trim() == username.Trim())
                    {
                        transactionsForPrint.Add(line);
                        j++;
                    }
                    
                }
                transactionsForPrint.RemoveRange(0, transactionsForPrint.Count/3);               
            }
            return transactionsForPrint;
        }
        public void CreditOnOff(int id)
        {
            foreach (Product product in Products.Products)
            {
                if (product.CompareTo(id) == 0)
                {
                    product.CanBeBoughtOnCredit = product.CanBeBoughtOnCredit == true ? false : true;
                }
            }
        }
        public void ActivateProduct(int id)
        {
            foreach(Product product in Products.Products)
            {
                if (product.CompareTo(id) == 0)
                {
                    product.Active = true;
                    GetActiveProducts();
                }

            }
        }
        public void DeactivateProduct(int id)
        {
            foreach (Product product in Products.Products)
            {
                if (product.CompareTo(id) == 0)
                {
                    product.Active = false;
                    GetActiveProducts();
                }
            }

        }
        public void InitiateStregsystem()
        {
            foreach (User user in Users.users)
            {
                user.LowBalance += this.OnLowBalance;
            }
        }
        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            var Transactions = new List<Transaction>();
            for (int i = 0; i < count; i++)
            {
                Transactions[i] = user.transactions[^(i + 1)];
            }
            return Transactions;
        }
        public void OnLowBalance(object source, UserArgs e)
        {
            OnLowBalanceStreg(e.user);
        }
        public void BuyProduct(User user, Product product)
        {
            Transaction transaction = new BuyTransaction(user, DateTime.Now, product.Price, product, Users.users);
            user.buyTransactions.Add(transaction);
            Transactions.Transactions.Add(transaction);
            Transactions.addTransactions();
        }
        public Transaction AddCreditsToAccount(string username, double amount)
        {

            foreach(User user in Users.users)
            {
                if(user.UserName == username)
                {
                    Transaction transaction = new InsertCashTransaction(user, DateTime.Now, amount, Users.users);
                    user.transactions.Add(transaction);
                    return transaction;
                }
            }
            throw new IndexOutOfRangeException("User does not exist");
        }
        public Product GetProductByID(int ID)
        {
            foreach (Product product in Products.Products)
            {
                if (ID == product.ID) return product;
            }
            throw new IndexOutOfRangeException("The product does not exist");
        }
        public void GetActiveProducts()
        {
            List<Product> ActiveProducts = new List<Product>();
            foreach (Product product in Products.Products)
            {
                if (product.Active) ActiveProducts.Add(product);
            }
            this.ActiveProducts = ActiveProducts;
        }
        public User GetUserByUsername(string username)
        {
            foreach (User user in Users.users)
            {
                if (username == user.UserName) return user;
            }
            throw new IndexOutOfRangeException("The User does not exist");
        }
        public List<User> GetUsers(Func<User, bool> predicate)
        {

            List<User> users = Users.users;
            return users.Where(predicate).ToList();
        }
    }
}
