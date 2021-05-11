using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiabloCms.Server.Infrastructure.Middleware
{
    public class ValidationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            if (exception is NullReferenceException)
            {
                code = HttpStatusCode.BadRequest;
                result = SerializeObject(new[] {"Invalid request."});
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            if (string.IsNullOrEmpty(result))
            {
                var error = exception.Message;

                result = SerializeObject(new[] {error});
            }

            return context.Response.WriteAsync(result);
        }

        private static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(true, true)
                }
            });
        }
    }
}