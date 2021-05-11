using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Addresses;
using DiabloCms.Models.ResponseModel.Addresses;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Addresses
{
    public interface IAddressesService
    {
        Task<string> CreateAsync(AddressRequestModel model, string userId);

        Task<Result> UpdateAsync(AddressRequestModel model, Guid id, string addressId);

        Task<Result> DeleteAsync(Guid id, string userId);

        Task<IEnumerable<AddressResponseModel>> ByUserAsync(string userId);
    }
}