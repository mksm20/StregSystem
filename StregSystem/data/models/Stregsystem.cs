﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class Stregsystem : IStregsystem
    {
        public Stregsystem() 
        {
            GetActiveProducts();
        }
        private UserList users = new UserList();
        private ProductList products = new ProductList();
        public IEnumerable<Product> ActiveProducts { get; private set;  }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            var Transactions = new List<Transaction>();
            return Transactions;
        }
        public void OnLowBalance(object source, UserArgs e)
        {
            throw new LowBalanceException($"You balance is {e.user.Balance} please consider refilling");
        }
        public BuyTransaction BuyProduct(User user,Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, DateTime.Now, product.Price, product);
            return transaction;
        }
        public InsertCashTransaction AddCreditsToAccount(User user,double amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, DateTime.Now, amount);
            return transaction;
        }
        public Product GetProductByID(int ID)
        {
            foreach(Product product in products.Products)
            {
                if (ID == product.ID) return product;
            }
            throw new IndexOutOfRangeException("The product does not exist");
        }
        public void GetActiveProducts()
        {
            List<Product> ActiveProducts = new List<Product>();
            foreach(Product product in products.Products)
            {
                if (product.Active) ActiveProducts.Add(product);
            }
            this.ActiveProducts = ActiveProducts;
        }
        public User GetUserByUsername(string username)
        {
            foreach(User user in users.users)
            {
                if (username == user.UserName) return user;
            }
            throw new IndexOutOfRangeException("The User does not exist");
        }
        public User GetUsers(Func<User,bool> Predicate)
        {
            User users;

            return users;
        } 
    }
}
