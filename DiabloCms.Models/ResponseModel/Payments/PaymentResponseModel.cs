using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Payments
{
    public class PaymentResponseModel : IMapFrom<Payment>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public float Percentage { get; set; }
    }
}