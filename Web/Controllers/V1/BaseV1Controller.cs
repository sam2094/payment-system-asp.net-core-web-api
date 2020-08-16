using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers.V1
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/v1/[controller]")]
	[ApiController]
	public class BaseV1Controller : BaseController
	{
		public AuthorizationFilterContext context;
        
	}
}
