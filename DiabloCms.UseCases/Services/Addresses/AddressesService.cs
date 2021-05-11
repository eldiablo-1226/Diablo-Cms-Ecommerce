using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Addresses;
using DiabloCms.Models.ResponseModel.Addresses;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Addresses;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Addresses
{
    using static ErrorMessagesService;

    public class AddressesService : BaseService<Address>, IAddressesService
    {
        public AddressesService(CmsDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<string> CreateAsync(AddressRequestModel model, string userId)
        {
            var address = new Address
            {
                UserId = userId,
                Country = model.Country,
                State = model.State,
                City = model.City,
                Description = model.Description,
                ZipCode = model.ZipCode,
                PhoneNumber = model.PhoneNumber
            };

            await Data.AddAsync(address).ConfigureAwait(false);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return address.Id.ToString();
        }

        public async Task<Result> UpdateAsync(AddressRequestModel model, Guid id, string userId)
        {
            var address = await FindByIdAndUserId(id, userId).ConfigureAwait(false);

            if (address == null) return InvalidErrorMessage;

            address.Country = model.Country;
            address.State = model.State;
            address.City = model.City;
            address.Description = model.Description;
            address.ZipCode = model.ZipCode;
            address.PhoneNumber = model.PhoneNumber;

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> DeleteAsync(Guid id, string userId)
        {
            var address = await FindByIdAndUserId(id, userId)
                .ConfigureAwait(false);

            if (address == null) return InvalidErrorMessage;

            Data.Remove(address);
            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<IEnumerable<AddressResponseModel>> ByUserAsync(string userId)
        {
            return await Mapper.ProjectTo<AddressResponseModel>(AllAsNoTracking
                    .Where(a => a.UserId == userId))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Address> FindByIdAndUserId(Guid id, string userId)
        {
            return await All
                .Where(a => a.Id == id && a.User.Id == userId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}