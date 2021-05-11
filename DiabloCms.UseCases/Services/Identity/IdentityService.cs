using System.Linq;
using System.Threading.Tasks;
using DiabloCms.Entities.Models;
using DiabloCms.Models.RequestModel.Identity;
using DiabloCms.Models.ResponseModel.Identity;
using DiabloCms.Shared;
using DiabloCms.Shared.ConstContent;
using DiabloCms.UseCases.Contracts.Identity;
using HarabaSourceGenerators.Common.Attributes;
using Microsoft.AspNetCore.Identity;

namespace DiabloCms.UseCases.Services.Identity
{
    using static ErrorMessagesService;

    [Inject]
    public partial class IdentityService : IIdentityService
    {
        private readonly IJwtGeneratorService _jwtGenerator;
        private readonly UserManager<CmsUser> _userManager;

        public async Task<Result> RegisterAsync(RegisterRequestModel model)
        {
            var user = new CmsUser
            {
                Email = model.Email,
                //PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email + "a"
            };

            var identity = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);

            if (identity.Succeeded)
            {
                return Result.Success;
            }

            var errorMessage = identity.Errors.Select(x => x.Description);
            return Result.Failure(errorMessage);
        }

        public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model)
        {
            //TODO add phone number identification

            var user = await _userManager.FindByEmailAsync(model.Email).ConfigureAwait(false);

            if (user == null) return InvalidLoginMessage;

            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false);

            if (!passwordValid) return InvalidLoginMessage;

            var token = await _jwtGenerator.GenerateJwtAsync(user).ConfigureAwait(false);

            return new LoginResponseModel {Token = token};
        }

        public async Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model, string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);

            if (user == null) return NotFound;
            if (model.NewPassword != model.ConfirmNewPassword) return "Unequal confirm password";

            var changePassword = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword)
                .ConfigureAwait(false);

            var errors = changePassword.Errors.Select(x => x.Description);

            return changePassword.Succeeded ? Result.Success : Result.Failure(errors);
        }

        public async Task<Result> ChangeUserSettingsAsync(ChangeSettingsRequestModel model, string id)
        {
            var user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);

            if (user == null) return NotFound;

            (user.FirstName, user.LastName) = (model.FirstName, model.LastName);

            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            var errors = result.Errors.Select(x => x.Description);

            return result.Succeeded ? Result.Success : Result.Failure(errors);
        }
    }
}