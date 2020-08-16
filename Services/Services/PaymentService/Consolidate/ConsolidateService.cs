using Common;
using Common.Enums.DatabaseEnums;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using Models.Dtos.PaymentDtos;
using Models.Entities;
using Models.Parameters.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public class ConsolidateService : AbstractService<ConsolidateOutput>, IConsolidateService<ConsolidateOutput>
    {
        public ConsolidateService(IUnitOfWork UoW, bool beginTransaction = false) :
               base(UoW, beginTransaction)
        {
        }

        public async Task<ContainerResult<ConsolidateOutput>> ConsolidateAsync(ConsolidateInput input, int currentUserId)
        {
            int orgId = _uow.UserRepository.GetAsync(x => x.Id == currentUserId, i => i.Branch).Result.Branch.OrgId;

            List<Payment> payments = null;

            if (input.FlagId == (byte)ConsolidateFlags.LIST_OF_PAYMENTS)
            {
                payments = _uow.PaymentRepository.GetAll(x => x.OrgId == orgId
                && x.CurrencyId == input.CurrencyId
                && x.SystemDate >= input.DateFrom && x.SystemDate <= input.DateTo, i => i.Currency, i => i.State).ToList();

                if (payments.Count != 0)
                {
                    Result.Output.Payments = new List<PaymentDto>(payments.Select(x => (PaymentDto)x));
                }
                else
                {
                    Result.Output.Payments = new List<PaymentDto>();
                }

                return Result;
            }
            else if (input.FlagId == (byte)ConsolidateFlags.PAYMENTS_SUM)
            {
                Result.Output.PaymentsSum = _uow.PaymentRepository.GetAll(x => x.OrgId == orgId
                && x.CurrencyId == input.CurrencyId
                && x.SystemDate >= input.DateFrom && x.SystemDate <= input.DateTo).Sum(x => x.Sum);

                Result.Output.PaymentsCount = _uow.PaymentRepository.GetAll(x => x.OrgId == orgId
                && x.CurrencyId == input.CurrencyId
                && x.SystemDate >= input.DateFrom && x.SystemDate <= input.DateTo).Count();
            }
            else
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INPUT_IS_NOT_VALID,
                    ErrorMessage = Resource.INVALID_INPUT,
                    StatusCode = ErrorHttpStatus.VALIDATION
                });

                return Result;
            }

            return Result;
        }
    }
}
