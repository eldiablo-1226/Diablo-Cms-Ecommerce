using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Orders
{
    public sealed class OrdersListingResponseModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string OrderName { get; set; }

        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public string Delivery { get; set; }

        public string CreateOn { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper
                .CreateMap<Order, OrdersListingResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(order => order.Id))
                .ForMember(x => x.OrderName, x
                    => x.MapFrom(order => "#" + order.Id.GetHashCode()))
                .ForMember(x => x.Status, x
                    => x.MapFrom(order => order.Status.ToString()))
                .ForMember(x => x.TotalPrice, x
                    => x.MapFrom(order => order.TotalTax))
                .ForMember(x => x.Delivery, x
                    => x.MapFrom(order => order.Delivery.Name))
                .ForMember(x => x.CreateOn, x
                    => x.MapFrom(order => order.CreatedOn.ToShortDateString()));
        }
    }
}