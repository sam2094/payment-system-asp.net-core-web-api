using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Web.Helpers;

namespace Web.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		public CreateActionResult<TResult> Result<TResult>(ContainerResult<TResult> result)
		{
			return new CreateActionResult<TResult>(result);
		}
	}
}
