using DataAccess.UnitofWork;
using Models.Entities;
using Models.Parameters.Payments;
using System;
using System.Threading.Tasks;
using Common;
using Common.Enums.DatabaseEnums;
using Common.Enums.ErrorEnums;
using Common.Resources;
using System.Linq;

namespace Services.Services.PaymentService
{
    public class CancellPaymentService : AbstractService<PaymentOutput>, ICancellPaymentService<PaymentOutput>
    {
        public CancellPaymentService(IUnitOfWork UoW, bool beginTransaction = true) :
              base(UoW, beginTransaction)
        {
        }

        public async Task<ContainerResult<PaymentOutput>> CancellPaymentAsync(CancellPaymentInput input, int currentUserId)
        {
            await BeginAsync(_beginTransaction);

            Payment payment = await _uow.PaymentRepository.GetAsync(x => x.Id == input.PaymentId);

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

            if (payment.StateId != (byte)PaymentStates.PAYED)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYMENT_DOES_NOT_PAYED,
                    ErrorMessage = Resource.PAYMENT_DOES_NOT_PAYED,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });
                return Result;
            }

            if (payment.SystemDate.AddMonths(2) < DateTime.Now)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYMENT_WAS_EXPIRED,
                    ErrorMessage = Resource.PAYMENT_WAS_EXPIRED,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });
                return Result;
            }

            // invoice account

            int orgId = _uow.UserRepository.GetAsync(x => x.Id == currentUserId, i => i.Branch).Result.Branch.OrgId;

            OrganisationAccount invoiceOrganisationAccount = _uow.GetRepository<OrganisationAccount>().GetAsync(x => x.AccountTypeId == (byte)AccountTypes.INVOICE
            && x.OrgId == orgId).Result;

            if (invoiceOrganisationAccount == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVOICE_ACCOUNT_DOES_NOT_EXIST,
                    ErrorMessage = Resource.INVOICE_ACCOUNT_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            Account invoiceAccount = await _uow.AccountRepository.GetAsync(x => x.Id == invoiceOrganisationAccount.AccountId);

            if (invoiceAccount == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVOICE_ACCOUNT_DOES_NOT_EXIST,
                    ErrorMessage = Resource.INVOICE_ACCOUNT_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            if (invoiceAccount.Balance - input.Sum < 0)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INSUFFICIENT_FUNDS,
                    ErrorMessage = Resource.INSUFFICIENT_FUNDS,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            if (input.Sum < invoiceAccount.PerTransMin)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVALID_MIN_TRANSACTION,
                    ErrorMessage = string.Format(Resource.INVALID_MIN_TRANSACTION, invoiceAccount.PerTransMin),
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            if (input.Sum > invoiceAccount.PerTransMax)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVALID_MAX_TRANSACTION,
                    ErrorMessage = string.Format(Resource.INVALID_MAX_TRANSACTION, invoiceAccount.PerTransMax),
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            OrganisationAccount payerOrganisationAccount = _uow.GetRepository<OrganisationAccount>().GetAsync(x => x.AccountTypeId == (byte)AccountTypes.PAYMENT
            && x.OrgId == payment.OrgId
            && x.CurrencyId == payment.CurrencyId).Result;

            if (payerOrganisationAccount == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYER_ACCOUNT_DOES_NOT_EXIST,
                    ErrorMessage = Resource.PAYER_ACCOUNT_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            Account payerAccount = await _uow.AccountRepository.GetAsync(x => x.Id == payerOrganisationAccount.AccountId);

            if (payerAccount == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYER_ACCOUNT_DOES_NOT_EXIST,
                    ErrorMessage = Resource.PAYER_ACCOUNT_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                });

                return Result;
            }

            // cash flow
            CashFlow cashFlow = new CashFlow
            {
                CashFrom = invoiceAccount.Id,
                CashTo = payerAccount.Id,
                SumFrom = input.Sum,
                SumTo = input.Sum,
                PairId = (int)CurrencyPairs.AZN_TO_AZN
            };

            await _uow.CashFlowRepository.AddAsync(cashFlow);

            invoiceAccount.Balance = invoiceAccount.Balance - input.Sum;
            payerAccount.Balance = payerAccount.Balance + input.Sum;

            await _uow.SaveChangesAsync();

            Payment newPayment = new Payment
            {
                TransId = input.TransId,
                InvoiceId = payment.InvoiceId,
                Sum = -input.Sum,
                ParentId = payment.Id,
                CurrencyId = payment.CurrencyId,
                ExternalDate = input.ExternalDate,
                Description = input.Description,
                OrgId = orgId,
                AppId = 13,
                StateId = (byte)PaymentStates.CANCELED,
                CashFlowId = cashFlow.Id
            };

            try
            {
                await _uow.PaymentRepository.AddAsync(newPayment);
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.PAYMENT_MUST_BE_UNIQUE,
                    ErrorMessage = Resource.PAYMENT_MUST_BE_UNIQUE,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            payment.StateId = (byte)PaymentStates.REVERSED;
            payment.UpdateDate = DateTime.Now;

            Invoice invoice = await _uow.InvoiceRepository.GetAsync(x => x.Id == payment.InvoiceId);

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

            if (input.Sum > invoice.Sum)
            {

                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.SUM_MORE_THAN_INVOICE_SUM,
                    ErrorMessage = Resource.SUM_MORE_THAN_INVOICE_SUM,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            invoice.RemainderSum = invoice.RemainderSum + input.Sum;
            invoice.StateId = (byte)InvoiceStates.ACTIVE;
            invoice.UpdateDate = DateTime.Now;

            await _uow.SaveChangesAsync();

            if (Result.ErrorList.Count > 0)
            {
                await RollBackAsync(_beginTransaction);

                return Result;
            }

            newPayment = await _uow.PaymentRepository.GetAsync(x => x.Id == newPayment.Id, i => i.State, i => i.Currency);

            await CommitAsync(_beginTransaction);

            Result.Output.Payment = newPayment;

            return Result;
        }
    }
}
