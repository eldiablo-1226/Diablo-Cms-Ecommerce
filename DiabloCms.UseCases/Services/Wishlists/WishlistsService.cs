using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.ResponseModel.Wishlists;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Wishlists;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Wishlists
{
    using static ErrorMessagesService;

    public class WishlistsService : BaseService<Wishlist>, IWishlistsService
    {
        public WishlistsService(CmsDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<Result> AddProductAsync(Guid id, string userId)
        {
            var hasProduct = await Data.Products
                .AnyAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (!hasProduct) return NotFound;

            var hasWishlist = await AllAsNoTracking
                .CountAsync(x => x.ProductId == id);

            if (hasWishlist > 0) return HasAlready;

            var wishlist = new Wishlist
            {
                UserId = userId,
                ProductId = id
            };

            await Data.AddAsync(wishlist).ConfigureAwait(false);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(Guid id, string userId)
        {
            var wishlist = await All
                .FirstOrDefaultAsync(w => w.Id == id)
                .ConfigureAwait(false);

            if (wishlist?.UserId != userId) return InvalidErrorMessage;
            if (wishlist == null) return NotFound;

            Data.Remove(wishlist);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<IEnumerable<WishlistResponseModel>> AllByUserId(string id)
        {
            return await Mapper.ProjectTo<WishlistResponseModel>(AllAsNoTracking
                    .Where(w => w.UserId == id))
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}