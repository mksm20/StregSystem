using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class Product : IToString
    {
        public Product(int iD, string name, double price, bool active, bool canBeBoughtOnCredit)
        {
            ID = iD;
            Name = name;
            priceChange(price);
            Active = active;
            CanBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public int ID { get; private set; }
        public string Name { get; set; }
        public double Price { get; private set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public delegate void NewProductAddedEventHandler(object source, ProductArgs args);
        public event NewProductAddedEventHandler NewProduct;
        protected virtual void OnNewProduct()
        {
            if (NewProduct != null) NewProduct(this, new ProductArgs() { product = this });
        }
        public string ToString(Product product)
        {
            string ObjparamsString = $"ID: {product.ID} Name: {product.Name} Price: {product.Price} \n";
            return ObjparamsString;
        }
        public void priceChange(double newPrice)
        {        
                if (newPrice > 0)
                {
                    this.Price = newPrice;
                }
                else
                {
                    throw new ArgumentException("Price cannot be less than 0");
                }
        }
    }
}
