using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Products
{
    public class PhotoDetailsResponseModel : IMapFrom<PhotoUrl>
    {
        public string Id { get; set; }
        public string Url { get; set; }

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<PhotoUrl, PhotoDetailsResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(photoUrl => photoUrl.Id))
                .ForMember(x => x.Url, x
                    => x.MapFrom(photoUrl => photoUrl.Files.Url));
        }
    }
}