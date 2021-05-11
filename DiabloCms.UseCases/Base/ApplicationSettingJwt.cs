using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DiabloCms.UseCases.Base
{
    public interface IApplicationSettingJwt
    {
        SymmetricSecurityKey GetSymmetricSecurityKey();
    }

    public class ApplicationSettingJwt : IApplicationSettingJwt
    {
        private const string Key = "56BC903172054DAA88E4669CABEF0A2F";

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.ASCII.GetBytes(Key));
        }
    }
}