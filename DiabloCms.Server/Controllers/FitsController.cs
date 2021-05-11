using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Fit;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Fit;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    [Authorize(Roles = CmsUserRoles.AdminRole)]
    public partial class FitsController : ApiController
    {
        private readonly IFitsService _fits;
        
        [HttpGet]
        public async Task<IEnumerable<Fit>> All()
        {
            return await _fits.AllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateFitsRequestModel model)
        {
            return await _fits
                .CreateFitAsync(model)
                .ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateFitsRequestModel model, Guid id)
        {
            return await _fits
                .UpdateFitAsync(model, id)
                .ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await _fits
                .DeleteFitAsync(id)
                .ToActionResult();
        }


        [HttpPost(nameof(CreateAttribute))]
        public async Task<ActionResult> CreateAttribute(Guid fitId, Guid productId)
        {
            return await _fits
                .AddFitItemAsync(fitId, productId)
                .ToActionResult();
        }

        [HttpDelete(nameof(DeleteAttribute))]
        public async Task<ActionResult> DeleteAttribute(Guid id)
        {
            return await _fits
                .DeleteFitItemAsync(id)
                .ToActionResult();
        }
    }
}