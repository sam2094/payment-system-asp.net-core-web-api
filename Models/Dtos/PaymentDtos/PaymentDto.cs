using Models.Entities;
using System;

namespace Models.Dtos.PaymentDtos
{
    public class PaymentDto
    {
        public long Id { get; set; }
        public string ExternalTransId { get; set; }
        public byte StateId { get; set; }
        public string StateName { get; set; }
        public decimal Sum { get; set; }
        public byte CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public DateTime SystemDate { get; set; }
        public DateTime ExternalDate { get; set; }

        public static implicit operator PaymentDto(Payment v)
        {
            return new PaymentDto
            {
                Id = v.Id,
                ExternalTransId = v.TransId,
                StateId = v.StateId,
                StateName = v.State.Name,
                Sum = v.Sum,
                CurrencyId = v.CurrencyId,
                CurrencyName = v.Currency.Name,
                SystemDate = v.SystemDate,
                ExternalDate = v.ExternalDate
            };
        }
    }
}
