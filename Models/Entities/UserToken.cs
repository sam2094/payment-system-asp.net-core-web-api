using System;

namespace Models.Entities
{
    public partial class UserToken : BaseEntity
    {
        public int Userid { get; set; }
        public string Token { get; set; }
        public DateTime GenerationDate { get; set; }
        public double LifePeriod { get; set; }
        public virtual User User { get; set; }
    }
}
