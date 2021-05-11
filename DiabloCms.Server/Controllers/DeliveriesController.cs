using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Deliveries;
using DiabloCms.Models.ResponseModel.Deliveries;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Deliveries;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class DeliveriesController : ApiController
    {
        private readonly IDeliveriesService _deliveries;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<DeliveryResponseModel>> All()
        {
            return await _deliveries.AllAsync();
        }

        [HttpPost]
        public async Task<string> Create(DeliveryRequastModel model)
        {
            return await _deliveries
                .CreateAsync(model);
        }

        [HttpPut]
        public async Task<ActionResult> Update(string id, DeliveryRequastModel model)
        {
            return await _deliveries
                .UpdateAsync(id, model)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            return await _deliveries
                .RemoveAsync(id)
                .ToActionResult();
        }
    }
}