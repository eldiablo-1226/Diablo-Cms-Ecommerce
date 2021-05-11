using System;
using System.ComponentModel.DataAnnotations;

namespace DiabloCms.Models.RequestModel.Orders
{
    public class OrdersRequestModel
    {
        [Required] public Guid AddressId { get; set; }

        [Required] public Guid DeliveryId { get; set; }

        [Required] public Guid PaymentId { get; set; }
    }
}