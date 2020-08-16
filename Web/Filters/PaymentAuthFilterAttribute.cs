using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Models.Parameters.AuthParams;
using Services.Services;
using Services.Services.AuthServices;
using Web.Helpers;

namespace Web.Filters
{
    public class PaymentAuthFilterAttribute : ActionFilterAttribute, IAsyncAuthorizationFilter
    {
        IAuthService _authService;

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_authService == null)
            {
                _authService = context.HttpContext.RequestServices.GetService(typeof(IAuthService)) as IAuthService;
            }

            context.HttpContext.Request.Headers.TryGetValue("AppId", out StringValues appid);
            context.HttpContext.Request.Headers.TryGetValue("Signature", out StringValues signatureData);
            context.HttpContext.Request.Headers.TryGetValue("Nonce", out StringValues nonce);

            var ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            PaymentAuthParam authparam = new PaymentAuthParam();

            authparam.Ip = ip;

            if (int.TryParse(appid, out int applicatID))
            {
                authparam.AppId = applicatID;
            }

            authparam.Signature = signatureData;
            authparam.Nonce = nonce;

            if (context.HttpContext.Request.Method.ToUpper() == "POST" ||
               context.HttpContext.Request.Method.ToUpper() == "PUT")
            {

                Stream req = context.HttpContext.Request.Body;
                using (StreamReader reader = new StreamReader(req, Encoding.UTF8, true, 1024, true))
                {
                    authparam.Body = await reader.ReadToEndAsync();
                }

            }

            var authresult = await _authService.AuthorizeAsync(authparam);

            if (authresult.Output == false)
            {
                context.Result = new CreateActionResult<string>(new ContainerResult<string>
                {
                    ErrorList = new List<Error>
                      {
                         new Error
                         {
                             ErrorCode = ErrorCodes.UNAUTHORIZED,
                             StatusCode = ErrorHttpStatus.UNAUTHORIZED,
                             ErrorMessage = Resource.UNAUTHORIZED
                         }
                      }
                })

                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };
            }
        }
    }
}
// nonce +  appid + body    || post,put methods 
// nonce +  appid           || get  methods 