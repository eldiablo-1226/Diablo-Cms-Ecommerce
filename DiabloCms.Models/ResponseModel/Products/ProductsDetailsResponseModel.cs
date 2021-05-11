using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Products
{
    public class ProductsDetailsResponseModel : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public string Article { get; set; }
        public string FitName { get; set; }
        public IEnumerable<string> Fits { get; set; }
        public IEnumerable<PhotoDetailsResponseModel> ImageSource { get; set; }
        public IEnumerable<AttributeResponseModel> Attributes { get; set; }

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Product, ProductsDetailsResponseModel>()
                .ForMember(x => x.Id, x
                    => x.MapFrom(product => product.Id))
                .ForMember(x => x.CategoryId, x
                    => x.MapFrom(product => product.CategoryId))
                .ForMember(x => x.CategoryName, x
                    => x.MapFrom(product => product.Category.Name))
                .ForMember(x => x.Name, x
                    => x.MapFrom(product => product.Name))
                .ForMember(x => x.ImageSource, x
                    => x.MapFrom(product => product.Photos))
                .ForMember(x => x.VideoUrl, x
                    => x.MapFrom(product => product.VideoUrl))
                .ForMember(x => x.Description, x
                    => x.MapFrom(product => product.Description))
                .ForMember(x => x.Article, x
                    => x.MapFrom(product => product.Article))
                .ForMember(x => x.Price, x
                    => x.MapFrom(product => product.Price))
                .ForMember(x => x.Attributes, x
                    => x.MapFrom(product => product.ProductAttributes))
                .ForMember(x => x.Fits, x
                    => x.MapFrom(product =>
                        product.FitIteams.FirstOrDefault().Fit.FitItems
                            .Select(fitIteam => fitIteam.ProductId.ToString())))
                .ForMember(x => x.FitName, x
                    => x.MapFrom(product => product.FitIteams.Select(fitIteam => fitIteam.Fit.Name).FirstOrDefault()));

            mapper.CreateMap<string, char>();
        }
    }
}