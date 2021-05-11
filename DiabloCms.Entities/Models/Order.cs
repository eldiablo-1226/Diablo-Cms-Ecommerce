using System;
using System.Collections.Generic;
using DiabloCms.Entities.Contracts;

namespace DiabloCms.Entities.Models
{
    public class Order : BaseDeletableModel
    {
        public string UserId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid DeliveryId { get; set; }
        public CmsUser User { get; set; }
        public Address Address { get; set; }
        public Payment Payment { get; set; }
        public Delivery Delivery { get; set; }

        public DateTime? Modified { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalTax { get; set; }

        public ICollection<OrderItem> OrderItems { get; } = new HashSet<OrderItem>();
    }
}