using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.CartIteam;
using DiabloCms.Models.ResponseModel.CartIteam;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.UseCases.Contracts.CartItems;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize]
    public partial class CartController : ApiController
    {
        private readonly ICartItemsService _cartItemsService;
        private readonly ICurrentUserService _currentUser;

        [HttpGet]
        public async Task<IEnumerable<CartItemResponseModel>> Mine()
        {
            return await _cartItemsService
                .ByUserAsync(_currentUser.UserId);
        }

        [HttpGet(nameof(TotalProducts))]
        public async Task<ActionResult<int>> TotalProducts()
        {
            return await _cartItemsService
                .TotalAsync(_currentUser.UserId);
        }

        [HttpGet(nameof(ProductStockQuantity))]
        public async Task<ActionResult<int>> ProductStockQuantity(Guid productId)
        {
            return await _cartItemsService
                .GetProductStockQuantityByIdAsync(productId, _currentUser.UserId);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(CartIteamRequestModel model)
        {
            return await _cartItemsService
                .AddProductAsync(model, _currentUser.UserId)
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int quantity, Guid id)
        {
            return await _cartItemsService
                .UpdateProductAsync(quantity, id, _currentUser.UserId)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveProduct(Guid id)
        {
            return await _cartItemsService
                .RemoveAsync(id, _currentUser.UserId)
                .ToActionResult();
        }
    }
}