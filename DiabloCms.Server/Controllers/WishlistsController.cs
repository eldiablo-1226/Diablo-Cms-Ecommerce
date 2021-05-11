using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.ResponseModel.Wishlists;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.UseCases.Contracts.Wishlists;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize]
    public partial class WishlistsController : ApiController
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IWishlistsService _wishlists;

        [HttpGet]
        public async Task<IEnumerable<WishlistResponseModel>> Mine()
        {
            return await _wishlists
                .AllByUserId(_currentUser.UserId);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _wishlists
                .RemoveAsync(id, _currentUser.UserId)
                .ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid id)
        {
            return await _wishlists
                .AddProductAsync(id, _currentUser.UserId)
                .ToActionResult();
        }
    }
}