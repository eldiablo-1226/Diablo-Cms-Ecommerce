using System;
using System.Threading.Tasks;
using DiabloCms.Models.ResponseModel.Products;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Products;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class ProductAttributeController : ApiController
    {
        private readonly IProductsService _products;

        [HttpPost]
        public async Task<ActionResult> Create(AttributeResponseModel model, Guid id)
        {
            return await _products
                .AddProductAttributeAsync(model, id)
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> Update(AttributeResponseModel model, Guid id)
        {
            return await _products
                .UpdateProductAttributeAsync(model, id)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _products
                .RemoveProductAttributeAsync(id)
                .ToActionResult();
        }
    }
}