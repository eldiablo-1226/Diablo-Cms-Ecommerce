using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.CartIteam;
using DiabloCms.Models.ResponseModel.CartIteam;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.CartItems;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.CartItems
{
    using static ErrorMessagesService;

    public class CartItemsService : BaseService<CartItem>, ICartItemsService
    {
        public CartItemsService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<CartItemResponseModel>> ByUserAsync(string userId)
        {
            return await Mapper
                .ProjectTo<CartItemResponseModel>(AllByUserId(userId))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result> AddProductAsync(CartIteamRequestModel model, string userId)
        {
            var isHas = await AllAsNoTracking
                .AnyAsync(x => x.ProductAttributeId == model.ProductAttributeId
                               && x.UserId == userId)
                .ConfigureAwait(false);

            var productId = model.ProductAttributeId;
            var quantity = model.Quantity;

            var productAttribute = await GetProductAttribute(productId)
                .ConfigureAwait(false);

            if (productAttribute == null) return NotFound;
            if (quantity > productAttribute.StockQuantity) return NotEnoughProductsMessage;

            if (!isHas)
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductAttribute = productAttribute,
                    Quantity = quantity
                };

                await Data.AddAsync(cartItem)
                    .ConfigureAwait(false);
            }
            else
            {
                var result = await All
                    .FirstOrDefaultAsync(x => x.ProductAttributeId == productId
                                              && x.UserId == userId)
                    .ConfigureAwait(false);

                if (result == null) return InvalidErrorMessage;

                result.Quantity = quantity;
            }

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> UpdateProductAsync(int quantity, Guid id, string userId)
        {
            var productAttribute = await All
                .Include(x => x.ProductAttribute)
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (productAttribute?.UserId != userId) return InvalidErrorMessage;
            if (productAttribute == null) return NotFound;
            if (quantity > productAttribute.ProductAttribute.StockQuantity) return NotEnoughProductsMessage;

            productAttribute.Quantity = quantity;

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(Guid id, string userId)
        {
            var cartITeam = await All
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (cartITeam?.UserId != userId) return InvalidErrorMessage;
            if (cartITeam == null) return NotFound;

            Data.Remove(cartITeam);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<int> TotalAsync(string userId)
        {
            return await AllByUserId(userId)
                .CountAsync()
                .ConfigureAwait(false);
        }

        public async Task<int> GetProductStockQuantityByIdAsync(Guid productId, string userId)
        {
            return await AllAsNoTracking
                .Where(x => x.ProductAttribute.ProductId == productId && x.UserId == userId)
                .CountAsync()
                .ConfigureAwait(false);
        }

        #region Helper

        private IQueryable<CartItem> AllByUserId(string userId)
        {
            return AllAsNoTracking
                .Where(c => c.UserId == userId);
        }

        private async Task<ProductAttribute> GetProductAttribute(Guid productId)
        {
            return await Data.ProductAttribute
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        #endregion
    }
}