using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Branch : BaseEntity
    {
        public Branch()
        {
            Invoices = new HashSet<Invoice>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool? State { get; set; }

        public virtual Organisation Org { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
