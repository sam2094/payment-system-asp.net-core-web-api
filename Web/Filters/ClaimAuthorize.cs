using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Security.Claims;
using Common;
using Common.Enums.DatabaseEnums;
using Common.Enums.ErrorEnums;
using Common.Resources;
using Web.Helpers;
using Services.Services.UserServices;
using Services.Services;
using Models.Parameters.User;

namespace Web.Filters
{
	public class ClaimAuthorize : Attribute, IAuthorizationFilter
	{
		private readonly Claims _claim;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="claim"></param>
		public ClaimAuthorize() { }
		public ClaimAuthorize(Claims claim)
		{
			_claim = claim;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
            IUserService<AuthorizationOutput> userService = context.HttpContext.RequestServices.GetService<IUserService<AuthorizationOutput>>();
            int id = Convert.ToInt32(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);

            if (userService.Authorization(new AuthorizationInput
            {
                PermissionId = (int)_claim,
                CurrentUserId = id
            }).Result.ErrorList.Count > 0)
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
