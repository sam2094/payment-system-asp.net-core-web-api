using Models.Entities;
using System;

namespace Models.Dtos.PaymentDtos
{
    public class VerifyTokenDto
    {
        public long InvoiceId { get; set; }
        public string OrderId { get; set; }
        public decimal Sum { get; set; }
        public decimal RemainderSum { get; set; }
        public byte CurrencyId { get; set; }
        public string BranchName { get; set; }
        public string OrgName { get; set; }
        public byte StateId { get; set; }
        public string StateName { get; set; }
        public string Info { get; set; }
        public string Email { get; set; }
        public DateTime ServerDate { get; set; }

        public static implicit operator VerifyTokenDto(Invoice v)
        {
            return new VerifyTokenDto
            {
               InvoiceId = v.Id,
               OrderId = v.OrderId,
               Sum = v.Sum,
               RemainderSum = v.RemainderSum,
               CurrencyId = v.CurrencyId,
               BranchName = v.Branch.Name,
               OrgName = v.Org.Name,
               StateId = v.StateId,
               StateName = v.State.Name,
               Info = v.Info,
               Email = v.Email,
               ServerDate = v.ServerDate
            };
        }
    }
}
