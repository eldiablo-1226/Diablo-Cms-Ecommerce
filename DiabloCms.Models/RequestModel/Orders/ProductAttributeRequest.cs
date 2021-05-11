using System;

namespace DiabloCms.Models.RequestModel.Orders
{
    public class ProductAttributeRequest
    {
        public Guid ProductAttributeId { get; set; }
        public int Quantity { get; set; }
    }
}