using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Payments;
using DiabloCms.Models.ResponseModel.Payments;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Payments
{
    public interface IPaymentsService
    {
        Task<Result> CreateAsync(PaymentRequastModel model);

        Task<Result> UpdateAsync(Guid id, PaymentRequastModel model);

        Task<Result> RemoveAsync(Guid id);

        Task<IEnumerable<PaymentResponseModel>> GetAllAsync();
    }
}