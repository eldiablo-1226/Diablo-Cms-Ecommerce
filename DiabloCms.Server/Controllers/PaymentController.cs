using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Payments;
using DiabloCms.Models.ResponseModel.Payments;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Payments;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class PaymentController : ApiController
    {
        private readonly IPaymentsService _payments;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PaymentResponseModel>> All()
        {
            return await _payments.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PaymentRequastModel model)
        {
            return await _payments
                .CreateAsync(model)
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid id, PaymentRequastModel model)
        {
            return await _payments
                .UpdateAsync(id, model)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _payments
                .RemoveAsync(id)
                .ToActionResult();
        }
    }
}