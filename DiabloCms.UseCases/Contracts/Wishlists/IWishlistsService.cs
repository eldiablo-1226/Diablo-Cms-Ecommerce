using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.ResponseModel.Wishlists;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Wishlists
{
    public interface IWishlistsService
    {
        Task<Result> AddProductAsync(Guid id, string userId);

        Task<Result> RemoveAsync(Guid id, string userId);

        Task<IEnumerable<WishlistResponseModel>> AllByUserId(string id);
    }
}