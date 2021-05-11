using System;

namespace DiabloCms.Entities.Models
{
    public class ProductProperty
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Property { get; set; }
        public string Value { get; set; }
    }
}