using Common;
using Common.Enums.DatabaseEnums;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using Models.Entities;
using Models.Parameters.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.PaymentService
{
    public class PaymentService : AbstractService<PaymentOutput>, IPaymentService<PaymentOutput>
    {
        public PaymentService(IUnitOfWork UoW, bool beginTransaction = true) :
               base(UoW, beginTransaction)
        {
        }

        public async Task<ContainerResult<PaymentOutput>> PaymentAsync(PaymentInput input, int currentUserId)
        {
            await BeginAsync(_beginTransaction);

            int orgId = _uow.UserRepository.GetAsync(x => x.Id == currentUserId, i => i.Branch).Result.Branch.OrgId;

            Invoice invoice = await _uow.InvoiceRepository.GetAsync(x => x.Id == input.InvoiceId && x.CurrencyId == input.CurrencyId);

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

            if (invoice.StateId == (byte) InvoiceStates.PAID) {

                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVOICE_PAID_OUT,
                    ErrorMessage = Resource.INVOICE_PAID_OUT,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            if (invoice.ExpDate < DateTime.Now)
            {

                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVOICE_EXPIRED,
                    ErrorMessage = Resource.INVOICE_EXPIRED,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            // payerAccount

            OrganisationAccount payerOrganisationAccount = _uow.GetRepository<OrganisationAccount>().GetAsync(x => x.AccountTypeId == (byte)AccountTypes.PAYMENT
            && x.OrgId == orgId
            && x.CurrencyId == input.CurrencyId).Result;

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

            if (payerAccount.Balance - input.Sum < 0)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INSUFFICIENT_FUNDS,
                    ErrorMessage = Resource.INSUFFICIENT_FUNDS,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            if (input.Sum < payerAccount.PerTransMin)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVALID_MIN_TRANSACTION,
                    ErrorMessage = string.Format(Resource.INVALID_MIN_TRANSACTION, payerAccount.PerTransMin),
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            if (input.Sum > payerAccount.PerTransMax)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INVALID_MAX_TRANSACTION,
                    ErrorMessage = string.Format(Resource.INVALID_MAX_TRANSACTION, payerAccount.PerTransMax),
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                });

                return Result;
            }

            #region monthly check
            //decimal currentMonthTransaction = _uow.CashFlowRepository.GetAll(x => x.CashFrom == payerAccount.Id 
            //     && x.Date.Month == DateTime.Now.Month)
            //    .Sum(i => i.SumFrom) + input.Sum;


            //if (currentMonthTransaction > payerAccount.MonthlySum) {

            //    Result.ErrorList.Add(new Error
            //    {
            //        ErrorCode = ErrorCodes.INVALID_MONTHLY_SUM,
            //        ErrorMessage = string.Format(Resource.INVALID_MONTHLY_SUM, payerAccount.MonthlySum),
            //        StatusCode = ErrorHttpStatus.FORBIDDEN
            //    });

            //    return Result;
            //}

            //if (currentMonthTransaction > payerAccount.MonthlyLimit)
            //{

            //    Result.ErrorList.Add(new Error
            //    {
            //        ErrorCode = ErrorCodes.INVALID_MONTHLY_LIMIT,
            //        ErrorMessage = string.Format(Resource.INVALID_MONTHLY_LIMIT, payerAccount.MonthlyLimit),
            //        StatusCode = ErrorHttpStatus.FORBIDDEN
            //    });

            //    return Result;
            //}
            #endregion

            // invoice account

            OrganisationAccount invoiceOrganisationAccount = _uow.GetRepository<OrganisationAccount>().GetAsync(x => x.AccountTypeId == (byte)AccountTypes.INVOICE
            && x.OrgId == invoice.OrgId
            && x.CurrencyId == invoice.CurrencyId).Result;

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

            // cash flow
            CashFlow cashFlow = new CashFlow
            {
                CashFrom = payerAccount.Id,
                CashTo = invoiceAccount.Id,
                SumFrom = input.Sum,
                SumTo = input.Sum,
                PairId = (int) CurrencyPairs.AZN_TO_AZN
            };

            await _uow.CashFlowRepository.AddAsync(cashFlow);
            await _uow.SaveChangesAsync();

            Payment payment = new Payment
            {
                TransId = input.ExternalTransId,
                InvoiceId = input.InvoiceId,
                Sum = input.Sum,
                CurrencyId = input.CurrencyId,
                ExternalDate = input.ExternalDate,
                ExternalPoint = input.ExternalPoint,
                CheckNumb = input.CheckNumb,
                Description = input.Description,
                OrgId = orgId,
                AppId = 13,
                StateId = (byte)PaymentStates.PAYED,
                CashFlowId = cashFlow.Id
            };

            try
            {
                await _uow.PaymentRepository.AddAsync(payment);
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


            if (Result.ErrorList.Count > 0)
            {
                await RollBackAsync(_beginTransaction);

                return Result;
            }

            Payment savedPayment = await _uow.PaymentRepository.GetAsync(x => x.Id == payment.Id, i => i.State, i => i.Currency);

            Result.Output.Payment = savedPayment;

            invoice.RemainderSum = invoice.RemainderSum - savedPayment.Sum;

            if (invoice.RemainderSum <= 0)
            {
                invoice.StateId = (byte)InvoiceStates.PAID;
            }

            payerAccount.Balance = payerAccount.Balance - savedPayment.Sum;
            payerAccount.Updated = DateTime.Now;

            invoiceAccount.Balance += savedPayment.Sum;
            invoiceAccount.Updated = DateTime.Now;

            await _uow.SaveChangesAsync();

            if (Result.ErrorList.Count > 0)
            {
                await RollBackAsync(_beginTransaction);

                return Result;
            }

            await CommitAsync(_beginTransaction);

            return Result;
        }
    }
}