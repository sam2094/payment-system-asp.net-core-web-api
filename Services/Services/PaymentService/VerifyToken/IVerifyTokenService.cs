using System.Threading.Tasks;


namespace Services.Services.PaymentService
{
    public interface IVerifyTokenService<TOutput>
    {
        Task<ContainerResult<TOutput>> VerifyTokenAsync(string token);
    }
}
