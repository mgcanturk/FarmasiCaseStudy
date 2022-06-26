using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Entities.Concreate
{
    public class Basket
    {
        public List<BasketItems> Items { get; set; } = new();
        public void AddProduct(Product product, int quantity)
        {
            var line = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line == null)
                Items.Add(new BasketItems() { Product = product, Quantity = quantity });
            else
                line.Quantity += quantity;
        }
        public void TakeOutProduct(Product product, int quantity)
        {
            var line = Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line != null)
            {
                line.Quantity -= quantity;
                if (line.Quantity <= 0)
                    DeleteProduct(product);
            }
        }
        public void DeleteProduct(Product product)
        {
            Items.RemoveAll(x => x.Product.Id == product.Id);
        }
        public void Clear()
        {
            Items.Clear();
        }
        public int TotalQuantity
        {
            get
            {
                return Items != null ? Items.Sum(x => x.Quantity) : 0;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                return Items != null ? Items.Sum(x => x.Product.Price * x.Quantity).GetValueOrDefault() : 0;
            }
        }
    }
}
