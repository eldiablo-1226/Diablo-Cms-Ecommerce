using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Addresses;
using DiabloCms.Models.ResponseModel.Addresses;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.UseCases.Contracts.Addresses;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize]
    public partial class AddressesController : ApiController
    {
        private readonly IAddressesService _addresses;
        private readonly ICurrentUserService _currentUser;

        [HttpGet]
        public async Task<IEnumerable<AddressResponseModel>> Mine()
        {
            return await _addresses
                .ByUserAsync(_currentUser.UserId);
        }

        [HttpPost(nameof(Create))]
        public async Task<string> Create(AddressRequestModel model)
        {
            return await _addresses
                .CreateAsync(model, _currentUser.UserId);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _addresses
                .DeleteAsync(id, _currentUser.UserId)
                .ToActionResult();
        }

        [HttpPut(nameof(Update))]
        public async Task<ActionResult> Update(AddressRequestModel model, Guid id)
        {
            return await _addresses
                .UpdateAsync(model, id, _currentUser.UserId)
                .ToActionResult();
        }
    }
}