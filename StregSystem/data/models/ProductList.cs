using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class ProductList
    {
        public List<Product> Products = new List<Product>();
        public void OnNewProduct(object source, ProductArgs e)
        {
            Products.Add(e.product);
        }
    }
}
