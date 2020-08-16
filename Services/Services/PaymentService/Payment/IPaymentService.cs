using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public interface IPaymentService<TOutput>
    {
        Task<ContainerResult<TOutput>> PaymentAsync(PaymentInput input, int currentUserId);
    }
}
