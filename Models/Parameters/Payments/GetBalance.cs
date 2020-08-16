using Models.Dtos.PaymentDtos;
using System.Collections.Generic;

namespace Models.Parameters.Payments
{
    public class GetBalanceOutput
    {
        public List<AccountDto> Accounts { get; set; }
    }
}
