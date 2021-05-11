using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DiabloCms.UseCases.Base
{
    public class ApplicationSettingJwt
    {
        public ApplicationSettingJwt(string key)
            => _key = key;
        
        private readonly string _key;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new(Encoding.ASCII.GetBytes(_key));
        }
    }
}