using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.CartIteam;
using DiabloCms.Models.ResponseModel.CartIteam;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.CartItems
{
    public interface ICartItemsService
    {
        Task<Result> AddProductAsync(CartIteamRequestModel model, string userId);

        Task<Result> UpdateProductAsync(int quantity, Guid id, string userId);

        Task<Result> RemoveAsync(Guid id, string userId);

        Task<IEnumerable<CartItemResponseModel>> ByUserAsync(string userId);

        Task<int> TotalAsync(string userId);

        Task<int> GetProductStockQuantityByIdAsync(Guid productId, string userId);
    }
}