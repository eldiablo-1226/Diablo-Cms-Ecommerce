using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Orders;
using DiabloCms.Models.ResponseModel.Orders;
using DiabloCms.MsSql;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Base;
using DiabloCms.UseCases.Contracts.Orders;
using Microsoft.EntityFrameworkCore;

namespace DiabloCms.UseCases.Services.Orders
{
    using static ErrorMessagesService;

    public class OrdersService : BaseService<Order>, IOrdersService
    {
        public OrdersService(CmsDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Result> CreateOrders(OrdersRequestModel model, string userId)
        {
            var order = new Order
            {
                UserId = userId,
                AddressId = model.AddressId,
                DeliveryId = model.DeliveryId,
                PaymentId = model.PaymentId,
                Status = OrderStatus.Pay
            };

            await Data.AddAsync(order)
                .ConfigureAwait(false);

            var productsAttributes = await Data
                .CartItem
                .Where(x => x.UserId == userId)
                .ToListAsync()
                .ConfigureAwait(false);

            if (productsAttributes.Count == 0) return EmptyCart;


            var payment = await Data.Payment.FindAsync(order.PaymentId);
            var delivery = await Data.Delivery.FindAsync(order.DeliveryId);

            var tax = await TotalTax(productsAttributes.Select(x => x.ProductAttributeId)).ConfigureAwait(false);
            if (payment.Percentage > 0) tax += tax * (decimal) payment.Percentage / 100;
            tax += delivery.Price;

            order.TotalTax = tax;

            var products = new List<OrderItem>();

            foreach (var orderModel in productsAttributes)
            {
                var guidId = orderModel.ProductAttributeId;

                var orderItem = new OrderItem
                {
                    Order = order,
                    ProductAttributeId = guidId,
                    Quantity = orderModel.Quantity
                };

                products.Add(orderItem);

                await ReduceProductQuantity(guidId, orderModel.Quantity)
                    .ConfigureAwait(false);
            }

            Data.RemoveRange(productsAttributes);

            await Data.OrderItem.AddRangeAsync(products)
                .ConfigureAwait(false);
            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<OrdersDetailsResponseModel> DetailsAsync(Guid id, string userId)
        {
            return await Mapper
                .ProjectTo<OrdersDetailsResponseModel>(AllAsNoTracking
                    .Where(o => o.Id == id && o.UserId == userId))
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<OrdersListingResponseModel>> ByUserAsync(string userId)
        {
            return await Mapper
                .ProjectTo<OrdersListingResponseModel>(AllAsNoTracking
                    .Where(o => o.UserId == userId))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result> ChangeStatus(Guid id, OrderStatus status)
        {
            var order = await All.FirstOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(false);

            if (order == null) return NotFound;

            order.Status = status;

            await Data.SaveChangesAsync()
                .ConfigureAwait(false);

            return Result.Success;
        }

        public async Task<decimal> TotalTax(string userId)
        {
            return await TotalTax(await Data.CartItem
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .ToArrayAsync()
                .ConfigureAwait(false));
        }

        private async Task ReduceProductQuantity(Guid productAttributeId, int requestQuantity)
        {
            var product = await Data
                .ProductAttribute
                .FindAsync(productAttributeId)
                .ConfigureAwait(false);

            product.StockQuantity -= requestQuantity;
        }

        private async Task<decimal> TotalTax(IEnumerable<Guid> products)
        {
            decimal tax = 0;

            await Data.ProductAttribute
                .Where(x => products
                    .Any(request => request == x.Id))
                .Select(x => x.Price)
                .ForEachAsync(x => tax += x)
                .ConfigureAwait(false);

            return tax;
        }
    }
}