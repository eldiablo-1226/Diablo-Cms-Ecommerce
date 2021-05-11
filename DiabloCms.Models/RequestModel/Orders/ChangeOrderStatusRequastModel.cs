using System.ComponentModel.DataAnnotations;
using DiabloCms.Entities.Models;

namespace DiabloCms.Models.RequestModel.Orders
{
    public class ChangeOrderStatusRequastModel
    {
        [Required] public string OrderId { get; set; }

        [Required] public OrderStatus Status { get; set; }
    }
}