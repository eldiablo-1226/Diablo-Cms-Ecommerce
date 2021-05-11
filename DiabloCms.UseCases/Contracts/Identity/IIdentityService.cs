using System.Threading.Tasks;
using DiabloCms.Models.RequestModel.Identity;
using DiabloCms.Models.ResponseModel.Identity;
using DiabloCms.Shared;

namespace DiabloCms.UseCases.Contracts.Identity
{
    public interface IIdentityService
    {
        Task<Result> RegisterAsync(RegisterRequestModel model);

        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);

        Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model, string id);

        Task<Result> ChangeUserSettingsAsync(ChangeSettingsRequestModel model, string id);
    }
}