using System;
using System.Linq;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Orders
{
    public class OrdersProductsResponseModel : IMapFrom<OrderItem>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string ImageSource { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper.CreateMap<OrderItem, OrdersProductsResponseModel>()
                .ForMember(x => x.ProductId, x
                    => x.MapFrom(orderItem => orderItem.ProductAttributeId))
                .ForMember(x => x.Name, x
                    => x.MapFrom(orderItem => orderItem.ProductAttribute.Product.Name))
                .ForMember(x => x.Color, x
                    => x.MapFrom(orderItem => orderItem.ProductAttribute.Color))
                .ForMember(x => x.Size, x
                    => x.MapFrom(orderItem => orderItem.ProductAttribute.Size))
                .ForMember(x => x.ImageSource, x
                    => x.MapFrom(orderItem => orderItem.ProductAttribute.Product.Photos.First().Files.Url))
                .ForMember(x => x.Quantity, x
                    => x.MapFrom(orderItem => orderItem.Quantity))
                .ForMember(x => x.Price, x
                    => x.MapFrom(orderItem => orderItem.ProductAttribute.Price));
        }
    }
}