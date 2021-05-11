using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Products
{
    public class ProductsListingResponseModel : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> ImageSource { get; set; }
        public IEnumerable<string> Colors { get; set; }

        public decimal Price { get; set; }

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Product, ProductsListingResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(product => product.Id))
                .ForMember(x => x.Name, x
                    => x.MapFrom(product => product.Name))
                .ForMember(x => x.ImageSource, x
                    => x.MapFrom(product => product.Photos.Select(photoUrl => photoUrl.Files.Url)))
                .ForMember(x => x.Colors, x
                    => x.MapFrom(
                        product => product.ProductAttributes.Select(productAttribute => productAttribute.Color)))
                .ForMember(x => x.Price, x
                    => x.MapFrom(product => product.Price));
        }
    }
}