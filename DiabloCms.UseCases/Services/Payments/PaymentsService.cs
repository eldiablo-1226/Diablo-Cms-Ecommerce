using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Payments;
using DiabloCms.Models.ResponseModel.Payments;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Payments;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Payments
{
    using static ErrorMessagesService;

    public class PaymentsService : BaseService<Payment>, IPaymentsService
    {
        public PaymentsService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Result> CreateAsync(PaymentRequastModel model)
        {
            var payment = new Payment
            {
                Name = model.Name,
                Logo = model.Logo,
                NormalizeName = model.NormalizeName,
                Percentage = model.Percentage
            };

            await Data.AddRangeAsync(payment).ConfigureAwait(false);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> UpdateAsync(Guid id, PaymentRequastModel model)
        {
            var payment = await All
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (payment == null) return NotFound;

            payment.Name = model.Name;
            payment.Logo = model.Logo;
            payment.NormalizeName = model.NormalizeName;
            payment.Percentage = model.Percentage;

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(Guid id)
        {
            var payment = await All
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (payment == null) return NotFound;

            Data.Remove(payment);

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<IEnumerable<PaymentResponseModel>> GetAllAsync()
        {
            return await Mapper.ProjectTo<PaymentResponseModel>(AllAsNoTracking)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}