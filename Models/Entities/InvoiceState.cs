using System.Collections.Generic;

namespace Models.Entities
{
    public partial class InvoiceState : BaseEntity
    {
        public InvoiceState()
        {
            Invoices = new HashSet<Invoice>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
