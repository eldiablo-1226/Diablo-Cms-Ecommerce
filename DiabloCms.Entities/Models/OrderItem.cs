using System;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class OrderItem : BaseDeletableModel
    {
        public Guid OrderId { get; set; }
        public Guid ProductAttributeId { get; set; }
        public Order Order { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
        public int Quantity { get; set; }
    }
}