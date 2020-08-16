using System.Threading.Tasks;

namespace Services.Services.PaymentService.Consolidate
{
    public interface IGetBalanceService<TOutput>
    {
        Task<ContainerResult<TOutput>> GetBalance();
    }
}
