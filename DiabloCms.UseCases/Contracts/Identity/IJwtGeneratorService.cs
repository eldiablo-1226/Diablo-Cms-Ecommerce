using System.Threading.Tasks;
using DiabloCms.Entities.Models;

namespace DiabloCms.UseCases.Contracts.Identity
{
    public interface IJwtGeneratorService
    {
        Task<string> GenerateJwtAsync(CmsUser user);
    }
}