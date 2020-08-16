using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Services.Services;

namespace Web.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TResult"></typeparam>

	public class CreateActionResult<TResult> : ObjectResult
	{
		private readonly HttpStatusCode _statusCode;
		private readonly ContainerResult<TResult> _result;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="result"></param>
		public CreateActionResult(ContainerResult<TResult> result) : base(result)
		{
			_result = result;
			_statusCode = result.ErrorList.Count > 0
				? (HttpStatusCode)(int)result.ErrorList.Select(x => x.StatusCode).Max()
				: HttpStatusCode.OK;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="statusCode"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public ObjectResult CreateResult()
		{
			return new ObjectResult(_result)
			{
				StatusCode = (int)_statusCode
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public Task ExecuteAsyncResult(ActionContext context)
		{
			return Task.FromResult(CreateResult());
		}
	}
}
