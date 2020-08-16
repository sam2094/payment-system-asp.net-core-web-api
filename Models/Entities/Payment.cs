using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Payment : BaseEntity
    {
        public Payment()
        {
            InverseParent = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string TransId { get; set; }
        public long InvoiceId { get; set; }
        public int AppId { get; set; }
        public int OrgId { get; set; }
        public byte StateId { get; set; }
        public string CheckNumb { get; set; }
        public decimal Sum { get; set; }
        public byte CurrencyId { get; set; }
        public long CashFlowId { get; set; }
        public DateTime SystemDate { get; set; }
        public string ExternalPoint { get; set; }
        public DateTime ExternalDate { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; }


        public virtual Application App { get; set; }
        public virtual CashFlow CashFlow { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual Organisation Org { get; set; }
        public virtual Payment Parent { get; set; }
        public virtual PaymentState State { get; set; }
        public virtual ICollection<Payment> InverseParent { get; set; }
    }
}
