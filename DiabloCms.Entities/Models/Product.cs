using System;
using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Product : BaseDeletableModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Article { get; set; }
        public bool IsActive { get; set; }
        public bool IsVariable { get; set; }
        public string VideoUrl { get; set; }

        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ProductProperty> ProductProperties { get; } = new HashSet<ProductProperty>();
        public ICollection<PhotoUrl> Photos { get; } = new HashSet<PhotoUrl>();
        public ICollection<ProductAttribute> ProductAttributes { get; } = new HashSet<ProductAttribute>();
        public ICollection<FitItem> FitIteams { get; } = new HashSet<FitItem>();
        public ICollection<Wishlist> Wishlists { get; } = new HashSet<Wishlist>();
    }
}