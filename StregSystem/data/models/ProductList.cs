using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            using (StreamReader r = new StreamReader(_path))
            {
                r.ReadLine();
                while (!r.EndOfStream)
                {
                    string temp = r.ReadLine();
                    string[] tempArr;
                    tempArr = temp.Split(";");
                    tempArr[1] = tempArr[1].Replace("<[^>]*>", "");
                    Product toBeAdded = new Product(
                        Int32.Parse(tempArr[0]),
                        tempArr[1],
                        Double.Parse(tempArr[2])/100,
                        Int32.Parse(tempArr[3]) != 0 ? true : false,
                        false
                   );
                    Products.Add(toBeAdded);
                }
            }
        }
        public void SaveProducts()
        {

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
