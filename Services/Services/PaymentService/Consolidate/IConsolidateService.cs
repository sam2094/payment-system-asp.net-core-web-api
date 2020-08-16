using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public interface IConsolidateService<TOutput>
    {
        Task<ContainerResult<TOutput>> ConsolidateAsync(ConsolidateInput input, int currentUserId);
    }
}
