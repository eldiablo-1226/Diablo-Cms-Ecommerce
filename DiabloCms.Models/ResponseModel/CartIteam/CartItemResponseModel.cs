using System.Linq;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.CartIteam
{
    public class CartItemResponseModel : IMapFrom<CartItem>
    {
        public string Id { get; set; }

        public string AttributeId { get; set; }

        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public int StockQuantity { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper.CreateMap<CartItem, CartItemResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.AttributeId, x
                    => x.MapFrom(x => x.ProductAttributeId.ToString()))
                .ForMember(x => x.ProductId, x
                    => x.MapFrom(x => x.ProductAttribute.Product.Id.ToString()))
                .ForMember(x => x.Name, x
                    => x.MapFrom(x => x.ProductAttribute.Product.Name))
                .ForMember(x => x.Price, x
                    => x.MapFrom(x => x.ProductAttribute.Price))
                .ForMember(x => x.ImageSource, x
                    => x.MapFrom(x => x.ProductAttribute.Product.Photos.FirstOrDefault().Files.Url))
                .ForMember(x => x.Quantity, x
                    => x.MapFrom(x => x.Quantity))
                .ForMember(x => x.StockQuantity, x
                    => x.MapFrom(x => x.ProductAttribute.StockQuantity));
        }
    }
}