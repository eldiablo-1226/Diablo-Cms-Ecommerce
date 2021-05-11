using System;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Products;
using DiabloCms.Models.ResponseModel.Products;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.HelperClass;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Products;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class ProductsController : ApiController
    {
        private readonly IProductsService _products;

        #region Product

        [HttpGet]
        [AllowAnonymous]
        public async Task<ProductsSearchResponseModel> Search([FromQuery] ProductsSearchRequestModel model)
        {
            return await _products.ProductSearchAsync(model);
        }

        [HttpGet(nameof(Details))]
        [AllowAnonymous]
        public async Task<ProductsDetailsResponseModel> Details(Guid id)
        {
            return await _products
                .DetailsAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductsRequestModel model)
        {
            return await _products
                .CreateProductAsync(model)
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateProductRequestModel model, Guid id)
        {
            return await _products
                .UpdateProductAsync(model, id)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _products
                .RemoveProductAsync(id)
                .ToActionResult();
        }

        #endregion

        #region Photo

        [HttpPost(nameof(AddPhoto))]
        public async Task<ActionResult> AddPhoto([FromForm] FormFileApiHelper photo, [FromQuery] Guid id)
        {
            var photoFile = photo.file;

            if (photoFile is null) return BadRequest();

            return await _products
                .AddProductPhotoAsync(photoFile.OpenReadStream(), id, photoFile.FileName)
                .ToActionResult();
        }

        [HttpDelete(nameof(DeletePhoto))]
        public async Task<ActionResult> DeletePhoto(Guid id)
        {
            return await _products
                .RemoveProductPhotoAsync(id)
                .ToActionResult();
        }

        #endregion
    }
}