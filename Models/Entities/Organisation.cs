






























































using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Organisation : BaseEntity
    {
        public Organisation()
        {
            AccountFee = new HashSet<AccountFee>();
            ActivityScope = new HashSet<ActivityScope>();
            Applications = new HashSet<Application>();
            Branches = new HashSet<Branch>();
            Invoices = new HashSet<Invoice>();
            OrganisationAccounts = new HashSet<OrganisationAccount>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte OrgType { get; set; }
        public int CityId { get; set; }
        public int? DetailsId { get; set; }
        public string Address { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }

        public virtual OrgTypes OrgTypeNavigation { get; set; }
        public virtual OrganisationDetail OrganisationDetails { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<AccountFee> AccountFee { get; set; }
        public virtual ICollection<ActivityScope> ActivityScope { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<OrganisationAccount> OrganisationAccounts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
