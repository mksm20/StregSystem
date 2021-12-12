using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class Stregsystem
    {
        public void BuyProduct(User user,Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, DateTime.Now, product.Price, product);
        }
        public void AddCreditsToAccount(User user,double amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, DateTime.Now, amount);
        }
        public static Product GetProductByID(int ID, ProductList productList)
        {
            foreach(Product product in productList.Products)
            {
                if (ID == product.ID) return product;
            }
            throw new IndexOutOfRangeException("The product does not exist");
        }
        public List<Product> ActiveProducts(ProductList products)
        {
            List<Product> ActiveProducts = new List<Product>();
            foreach(Product product in products.Products)
            {
                if (product.Active) ActiveProducts.Add(product);
            }
            return ActiveProducts;
        }

    }
}
