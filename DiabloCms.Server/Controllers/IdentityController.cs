using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Identity;
using DiabloCms.Models.ResponseModel.Identity;
using DiabloCms.Server.Infrastructure.Extensions;
using DiabloCms.Server.Infrastructure.Service.CurrentUser;
using DiabloCms.UseCases.Contracts.Identity;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabloCms.Server.Controllers
{
    [Inject]
    public partial class IdentityController : ApiController
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IIdentityService _identity;

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            return await _identity
                .RegisterAsync(model)
                .ToActionResult();
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            return await _identity
                .LoginAsync(model)
                .ToActionResult();
        }

        [Authorize]
        [HttpPut(nameof(ChangeSettings))]
        public async Task<ActionResult> ChangeSettings(ChangeSettingsRequestModel model)
        {
            return await _identity
                .ChangeUserSettingsAsync(model, _currentUser.UserId)
                .ToActionResult();
        }

        [Authorize]
        [HttpPut(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestModel model)
        {
            return await _identity
                .ChangePasswordAsync(model, _currentUser.UserId)
                .ToActionResult();
        }
    }
}