using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    class ProductEnumerator : IEnumerator<Product>
    {
        private ProductList _productList;
        private int _idx;
        public ProductEnumerator(ProductList producList)
        {
            _productList = producList;
            _idx = -1;
        }
        public Product Current => _productList.Products[_idx];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            return ++_idx < _productList.Products.Count();
        }

        public void Reset()
        {
            _idx = -1;
            throw new NotImplementedException();
        }
    }
}
