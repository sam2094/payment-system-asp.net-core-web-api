using System;

namespace Models.Dtos.PaymentDtos
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
