using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StregSystem.data.models
{

   
    public class ProductList : IEnumerable<Product>
    {
        public ProductList()
        {
            addProducts();
        }
        public List<Product> Products = new List<Product>();
        private string _path = "../../../files/products.csv";
    
        private void addProducts() 
        {
            const string htmlTagPattern = "<.*?>";
            using (StreamReader r = new StreamReader(_path))
            {
                r.ReadLine();
                while (!r.EndOfStream)
                {
                    string temp = r.ReadLine();
                    string[] tempArr;
                    tempArr = temp.Split(";");
                    tempArr[1] = Regex.Replace(tempArr[1], htmlTagPattern, string.Empty);
                    Product toBeAdded = new Product(
                        Int32.Parse(tempArr[0]),
                        tempArr[1],
                        Double.Parse(tempArr[2]),
                        bool.Parse(tempArr[3]),
                        bool.Parse(tempArr[4])
                   );
                    Products.Add(toBeAdded);
                }
            }
        }
        public void SaveProducts()
        {
            using (StreamWriter w = new StreamWriter(_path))
            {
                foreach(Product product in Products)
                {
                    string toWrite = ($"{product.ID};{product.Name};{product.Price};{product.Active};{product.CanBeBoughtOnCredit}");
                    w.WriteLine(toWrite);
                }
            }
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return new ProductEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
