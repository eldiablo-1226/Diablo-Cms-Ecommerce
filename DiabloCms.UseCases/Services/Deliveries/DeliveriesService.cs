using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Deliveries;
using DiabloCms.Models.ResponseModel.Deliveries;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Deliveries;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Deliveries
{
    using static ErrorMessagesService;

    public class DeliveriesService : BaseService<Delivery>, IDeliveriesService
    {
        public DeliveriesService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<string> CreateAsync(DeliveryRequastModel model)
        {
            var delivery = new Delivery
            {
                Name = model.Name,
                Logo = model.Logo,
                Price = model.Price
            };

            await Data.AddAsync(delivery).ConfigureAwait(false);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return delivery.Id.ToString();
        }

        public async Task<Result> UpdateAsync(string id, DeliveryRequastModel model)
        {
            var delivery = await FindById(Guid.Parse(id)).ConfigureAwait(false);

            if (delivery == null) return NotFound;

            delivery.Name = model.Name;
            delivery.Logo = model.Logo;
            delivery.Price = model.Price;

            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<Result> RemoveAsync(string id)
        {
            var delivery = await FindById(Guid.Parse(id)).ConfigureAwait(false);

            if (delivery == null) return NotFound;

            Data.Remove(delivery);
            await Data.SaveChangesAsync().ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<IEnumerable<DeliveryResponseModel>> AllAsync()
        {
            return await Mapper.ProjectTo<DeliveryResponseModel>(AllAsNoTracking)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Delivery> FindById(Guid id)
        {
            return await All
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}