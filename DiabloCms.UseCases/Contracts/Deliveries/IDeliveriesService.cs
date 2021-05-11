using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Deliveries;
using DiabloCms.Models.ResponseModel.Deliveries;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Deliveries
{
    public interface IDeliveriesService
    {
        Task<string> CreateAsync(DeliveryRequastModel model);

        Task<Result> UpdateAsync(string id, DeliveryRequastModel model);

        Task<Result> RemoveAsync(string id);

        Task<IEnumerable<DeliveryResponseModel>> AllAsync();
    }
}