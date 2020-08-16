using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class CashFlow : BaseEntity
    {
        public CashFlow()
        {
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public int CashFrom { get; set; }
        public int CashTo { get; set; }
        public int PairId { get; set; }
        public decimal SumFrom { get; set; }
        public decimal SumTo { get; set; }
        public DateTime Date { get; set; }

        public virtual Account CashFromNavigation { get; set; }
        public virtual Account CashToNavigation { get; set; }
        public virtual CurrencyPair Pair { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
