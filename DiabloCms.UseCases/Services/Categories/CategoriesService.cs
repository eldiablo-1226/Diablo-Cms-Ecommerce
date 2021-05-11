using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Categories;
using DiabloCms.Models.ResponseModel.Categories;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Categories;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Categories
{
    using static ErrorMessagesService;

    public class CategoriesService : BaseService<Category>, ICategoriesService
    {
        public CategoriesService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<string> CreateAsync(CategoryRequastModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                ParentCategoryName = model.ParentCategoryName,
                ShowInFilter = model.ShowInFilter
            };

            await Data.AddAsync(category).ConfigureAwait(false);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return category.Id.ToString();
        }

        public async Task<Result> UpdateAsync(Guid id, CategoryRequastModel model)
        {
            var category = await FindById(id)
                .ConfigureAwait(false);

            if (category == null) return NotFound;

            category.Name = model.Name;
            category.ParentCategoryName = model.ParentCategoryName;
            category.ShowInFilter = model.ShowInFilter;

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var category = await FindById(id)
                .ConfigureAwait(false);

            if (category == null) return NotFound;

            Data.Remove(category);
            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<IEnumerable<CategoryResponseModel>> AllAsync()
        {
            return await Mapper
                .ProjectTo<CategoryResponseModel>(AllAsNoTracking)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Category> FindById(Guid id)
        {
            return await All
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}