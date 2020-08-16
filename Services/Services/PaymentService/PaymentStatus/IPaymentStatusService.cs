using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
   public interface IPaymentStatusService<TOutput>
    {
        Task<ContainerResult<TOutput>> GetPaymentStatusAsync(PaymentStatusInput input, int currentUserId);
    }
}
