using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Categories;
using DiabloCms.Models.ResponseModel.Categories;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Categories;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class CategoriesController : ApiController
    {
        private readonly ICategoriesService _categories;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CategoryResponseModel>> All()
        {
            return await _categories.AllAsync();
        }

        [HttpPost]
        public async Task<string> Create(CategoryRequastModel model)
        {
            return await _categories
                .CreateAsync(model);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid id, CategoryRequastModel model)
        {
            return await _categories
                .UpdateAsync(id, model)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _categories
                .RemoveAsync(id)
                .ToActionResult();
        }
    }
}