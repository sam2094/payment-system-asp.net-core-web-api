using System;

namespace Models.Entities
{
    public partial class CurrencyRate : BaseEntity
    {
        public long Id { get; set; }
        public int PairId { get; set; }
        public decimal Nominal { get; set; }
        public decimal Rate { get; set; }
        public DateTime Day { get; set; }
        public bool State { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }

        public virtual CurrencyPair Pair { get; set; }
    }
}
