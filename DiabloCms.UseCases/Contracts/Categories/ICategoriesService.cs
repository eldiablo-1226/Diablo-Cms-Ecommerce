using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Categories;
using DiabloCms.Models.ResponseModel.Categories;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Categories
{
    public interface ICategoriesService
    {
        Task<string> CreateAsync(CategoryRequastModel model);

        Task<Result> UpdateAsync(Guid id, CategoryRequastModel model);

        Task<Result> RemoveAsync(Guid id);

        Task<IEnumerable<CategoryResponseModel>> AllAsync();
    }
}