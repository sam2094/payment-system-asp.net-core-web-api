using System.Threading.Tasks;
using Models.Parameters.User;

namespace Services.Services.UserServices
{
	public interface IUserService<TOutput>
	{
		Task<ContainerResult<TOutput>> Authorization(AuthorizationInput input);
	}
}
