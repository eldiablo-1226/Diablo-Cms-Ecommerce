using System;
using DiabloCms.Entities.Models;
using DiabloCms.Models.Mapper;

namespace DiabloCms.Models.ResponseModel.Categories
{
    public class CategoryResponseModel : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ParentCategoryName { get; set; }
    }
}