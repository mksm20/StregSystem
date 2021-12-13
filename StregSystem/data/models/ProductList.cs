using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class ProductList : IEnumerable<Product>
    {
        public List<Product> Products = new List<Product>();
        public void OnNewProduct(object source, ProductArgs e)
        {
            Products.Add(e.product);
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
