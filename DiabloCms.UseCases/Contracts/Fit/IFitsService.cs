using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Fit;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Fit
{
    public interface IFitsService
    {
        Task<IEnumerable<Entities.Models.Fit>> AllAsync();
        Task<Result> CreateFitAsync(CreateFitsRequestModel model);
        Task<Result> UpdateFitAsync(UpdateFitsRequestModel model, Guid id);
        Task<Result> DeleteFitAsync(Guid id);
        Task<Result> AddFitItemAsync(Guid fitId, Guid productId);
        Task<Result> DeleteFitItemAsync(Guid fitItemId);
    }
}