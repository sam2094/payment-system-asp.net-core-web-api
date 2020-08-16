using Common.Enums.DatabaseEnums;
using DataAccess.UnitofWork;
using Models.Dtos.PaymentDtos;
using Models.Entities;
using Models.Parameters.Payments;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.PaymentService.Consolidate
{
    public class GetBalanceService : AbstractService<GetBalanceOutput>, IGetBalanceService<GetBalanceOutput>
    {
        public GetBalanceService(IUnitOfWork UoW, bool beginTransaction = false) :
               base(UoW, beginTransaction)
        {
        }

        public async Task<ContainerResult<GetBalanceOutput>> GetBalance()
        {
            Result.Output.Accounts = _uow.GetRepository<OrganisationAccount>().GetAll(i => i.Account, i => i.Currency).Select(x => new AccountDto
            {
                AccountId = x.AccountId,
                Name = x.Account.Name,
                Balance = x.Account.Balance,
                CurrencyId = x.CurrencyId,
                CurrencyName = x.Currency.Name,
                State = x.Account.State == true ? AccountStates.ACTIVE.ToString() : AccountStates.BLOCKED.ToString(),
                Created = x.Account.Created,
                Updated = x.Account.Updated
            }).ToList();

            return Result;
        }
    }
}
