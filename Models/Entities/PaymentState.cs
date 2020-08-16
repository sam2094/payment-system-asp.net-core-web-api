using System.Collections.Generic;

namespace Models.Entities
{
    public partial class PaymentState : BaseEntity
    {
        public PaymentState()
        {
            Payments = new HashSet<Payment>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
