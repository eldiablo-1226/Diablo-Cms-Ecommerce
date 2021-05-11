using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Orders;
using DiabloCms.Models.ResponseModel.Orders;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Orders;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize]
    public partial class OrdersController : ApiController
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IOrdersService _ordersService;

        [HttpGet]
        public async Task<IEnumerable<OrdersListingResponseModel>> Mine()
        {
            return await _ordersService
                .ByUserAsync(_currentUser.UserId);
        }


        [HttpGet(nameof(ProductTotalTax))]
        public async Task<decimal> ProductTotalTax()
        {
            return await _ordersService
                .TotalTax(_currentUser.UserId);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create(OrdersRequestModel model)
        {
            return await _ordersService
                .CreateOrders(model, _currentUser.UserId)
                .ToActionResult();
        }

        [HttpGet(nameof(Details))]
        public async Task<OrdersDetailsResponseModel> Details(Guid id)
        {
            return await _ordersService
                .DetailsAsync(id, _currentUser.UserId);
        }

        [HttpPut]
        [Authorize(Roles = CmsUserRoles.AdminRole)]
        public async Task<ActionResult> ChangeStatus(Guid id, OrderStatus status)
        {
            return await _ordersService
                .ChangeStatus(id, status)
                .ToActionResult();
        }
    }
}