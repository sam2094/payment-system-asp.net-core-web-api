using Models.Parameters.AuthParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.AuthServices
{
    public interface IAuthService
    {

        Task<ContainerResult<bool>> AuthorizeAsync(PaymentAuthParam input);

    }
}
