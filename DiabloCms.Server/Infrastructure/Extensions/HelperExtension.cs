using Microsoft.AspNetCore.Http;

namespace DiabloCms.Server.Infrastructure.Extensions
{
    public static class HelperExtension
    {
        public static string GetAbsoluteUrl(this HttpContext currentContext)
        {
            var request = currentContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}