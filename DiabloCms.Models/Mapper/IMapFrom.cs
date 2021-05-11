using AutoMapper;

namespace DiabloCms.Models.Mapper
{
    public interface IMapFrom<TModel> where TModel : class
    {
        void Mapping(Profile mapper)
        {
            mapper.CreateMap(typeof(TModel), GetType());
        }
    }
}