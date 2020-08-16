using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Invoice : BaseEntity
    {
        public Invoice()
        {
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public string Value { get; set; }
        public string OrderId { get; set; }
        public decimal Sum { get; set; }
        public decimal RemainderSum { get; set; }
        public byte CurrencyId { get; set; }
        public int AppId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int OrgId { get; set; }
        public byte StateId { get; set; }
        public string Info { get; set; }
        public string Email { get; set; }
        public string Ip { get; set; }
        public DateTime ServerDate { get; set; }
        public DateTime SenderDate { get; set; }
        public DateTime ExpDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Application App { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Organisation Org { get; set; }
        public virtual InvoiceState State { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
