using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using Models.Entities;
using Models.Parameters.Payments;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public class VerifyTokenService : AbstractService<VerifyTokenOutput>, IVerifyTokenService<VerifyTokenOutput>
    {
        public VerifyTokenService(IUnitOfWork UoW, bool beginTransaction = false) :
               base(UoW, beginTransaction)
        { }

        public async Task<ContainerResult<VerifyTokenOutput>> VerifyTokenAsync(string token)
        {
            Invoice invoice = await _uow.InvoiceRepository.GetAsync(x => x.Value == token, i => i.Branch, i => i.Org, i => i.State);

            if (invoice == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVOICE_DOES_NOT_EXIST,
                    ErrorMessage = Resource.INVOICE_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            Result.Output.VerifyTokenResult = invoice;

            return Result;
        }
    }
}
