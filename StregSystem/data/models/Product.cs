using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.data.models
{
    public class Product : IComparable<Product>
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
        public override string ToString()
        {
            string ObjparamsString = $"ID: {this.ID} Name: {this.Name} Price: {this.Price} \n";
            return ObjparamsString;
        }
        public void priceChange(double newPrice)
        {        
                if (newPrice >= 0)
                {
                    this.Price = newPrice;
                }
                else
                {
                    throw new ArgumentException("Price cannot be less than 0");
                }
        }
        public int CompareTo(Product that)
        {
            if (this.ID < that.ID) return -1;
            if (this.ID == that.ID) return 0;
            return 1;
        }
    }
}
