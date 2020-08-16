using System.Collections.Generic;

namespace Models.Entities
{
    public partial class CurrencyPair : BaseEntity
    {
        public CurrencyPair()
        {
            CashFlow = new HashSet<CashFlow>();
            CurrencyRates = new HashSet<CurrencyRate>();
        }

        public int Id { get; set; }
        public byte BaseCurrency { get; set; }
        public byte SecondaryCurrency { get; set; }
        public bool? State { get; set; }

        public virtual Currency BaseCurrencyNavigation { get; set; }
        public virtual Currency SecondaryCurrencyNavigation { get; set; }
        public virtual ICollection<CashFlow> CashFlow { get; set; }
        public virtual ICollection<CurrencyRate> CurrencyRates { get; set; }
    }
}
