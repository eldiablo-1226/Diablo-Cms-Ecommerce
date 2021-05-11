using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Deliveries
{
    public class DeliveryResponseModel : IMapFrom<Delivery>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public decimal Price { get; set; }
    }
}