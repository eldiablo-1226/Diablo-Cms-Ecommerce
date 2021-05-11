using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Orders;
using DiabloCms.Models.ResponseModel.Orders;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Orders
{
    public interface IOrdersService
    {
        Task<Result> CreateOrders(OrdersRequestModel model, string userId);

        Task<OrdersDetailsResponseModel> DetailsAsync(Guid id, string userId);

        Task<IEnumerable<OrdersListingResponseModel>> ByUserAsync(string userId);

        Task<Result> ChangeStatus(Guid id, OrderStatus status);

        Task<decimal> TotalTax(string userId);
    }
}