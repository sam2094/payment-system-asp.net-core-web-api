using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using Models.Entities;
using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public class PaymentStatusService : AbstractService<PaymentOutput>, IPaymentStatusService<PaymentOutput>
    {
        public PaymentStatusService(IUnitOfWork UoW, bool beginTransaction = true) :
               base(UoW, beginTransaction)
        {
        }

        public async Task<ContainerResult<PaymentOutput>> GetPaymentStatusAsync(PaymentStatusInput input, int currentUserId)
        {

            if (input.PaymentId <= 0 && input.ExternalTransactionId == null || input.ExternalTransactionId?.Trim().Length == 0)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INPUT_IS_NOT_VALID,
                    ErrorMessage = Resource.INVALID_INPUT,
                    StatusCode = ErrorHttpStatus.VALIDATION
                });

                return Result;
            }

            Payment payment = null;
            int orgId = _uow.UserRepository.GetAsync(x => x.Id == currentUserId, i => i.Branch).Result.Branch.OrgId;

            if (input.PaymentId != 0)
            {
                payment = await _uow.PaymentRepository.GetAsync(x => x.Id == input.PaymentId && x.OrgId == orgId, i => i.State, i => i.Currency);
            }
            else
            {
                payment = await _uow.PaymentRepository.GetAsync(x => x.TransId == input.ExternalTransactionId && x.OrgId == orgId, i => i.State, i => i.Currency);
            }

            if (payment == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYMENT_DOES_NOT_EXIST,
                    ErrorMessage = Resource.PAYMENT_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            Result.Output.Payment = payment;

            return Result;
        }
    }
}
