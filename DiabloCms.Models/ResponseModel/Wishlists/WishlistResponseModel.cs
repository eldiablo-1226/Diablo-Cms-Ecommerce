using System.Linq;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Wishlists
{
    public class WishlistResponseModel : IMapFrom<Wishlist>
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageSource { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper.CreateMap<Wishlist, WishlistResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(wishlist => wishlist.Id.ToString()))
                .ForMember(x => x.ProductId, x
                    => x.MapFrom(wishlist => wishlist.ProductId.ToString()))
                .ForMember(x => x.Name, x
                    => x.MapFrom(wishlist => wishlist.Product.Name))
                .ForMember(x => x.Price, x
                    => x.MapFrom(wishlist => wishlist.Product.Price))
                .ForMember(x => x.ImageSource, x
                    => x.MapFrom(x => x.Product.Photos.First().Files.Url));
        }
    }
}