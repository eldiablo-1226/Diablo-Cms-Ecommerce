using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Fit;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Fit;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Fits
{
    using static ErrorMessagesService;

    public class FitsService : BaseService<Fit>, IFitsService
    {
        public FitsService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Fit>> AllAsync()
        {
            return await Data.Fit
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result> CreateFitAsync(CreateFitsRequestModel model)
        {
            var fit = new Fit
            {
                Name = model.Name,
                Photo = model.Photo
            };

            await Data.AddAsync(fit)
                .ConfigureAwait(false);

            fit.FitItems = model.ProductId.Select(x => new FitItem
            {
                ProductId = x,
                FitId = fit.Id
            }).ToArray();

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> UpdateFitAsync(UpdateFitsRequestModel model, Guid id)
        {
            var result = await Data.Fit.FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (result == null) return NotFound;

            result.Name = model.Name;
            result.Photo = model.Photo;

            return Result.Success;
        }

        public async Task<Result> DeleteFitAsync(Guid id)
        {
            var result = await Data.Fit
                .FirstOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);

            if (result == null) return NotFound;

            Data.Remove(result);

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> AddFitItemAsync(Guid fitId, Guid productId)
        {
            var result = await Data.Fit
                .FirstOrDefaultAsync(x => x.Id == fitId)
                .ConfigureAwait(false);

            if (result == null) return NotFound;

            var hasProduct = await Data.Products
                .AnyAsync(x => x.Id == productId)
                .ConfigureAwait(false);

            if (hasProduct) return NotFound;

            var newFitItem = new FitItem
            {
                ProductId = productId,
                FitId = result.Id
            };

            result.FitItems.Add(newFitItem);

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> DeleteFitItemAsync(Guid fitItemId)
        {
            var result = await Data.FitIteam
                .FirstOrDefaultAsync(x => x.Id == fitItemId)
                .ConfigureAwait(false);

            if (result == null) return NotFound;

            Data.Remove(result);
            await Data.SaveChangesAsync();

            return Result.Success;
        }
    }
}