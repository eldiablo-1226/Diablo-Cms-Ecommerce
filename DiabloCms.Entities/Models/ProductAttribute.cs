using System;
using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class ProductAttribute : BaseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Photo { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<CartItem> CartItems { get; } = new HashSet<CartItem>();

        public ICollection<OrderItem> OrderItems { get; } = new HashSet<OrderItem>();
    }
}