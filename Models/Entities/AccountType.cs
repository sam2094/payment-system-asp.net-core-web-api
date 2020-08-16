using System.Collections.Generic;

namespace Models.Entities
{
    public partial class AccountType : BaseEntity
    {
        public AccountType()
        {
            AccountFee = new HashSet<AccountFee>();
            OrganisationAccounts = new HashSet<OrganisationAccount>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AccountFee> AccountFee { get; set; }
        public virtual ICollection<OrganisationAccount> OrganisationAccounts { get; set; }
    }
}
