using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public interface ICancellPaymentService<TOutput>
    {
        Task<ContainerResult<TOutput>> CancellPaymentAsync(CancellPaymentInput input, int currentUserId);
    }
}
