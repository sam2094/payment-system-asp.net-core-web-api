using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Account : BaseEntity
    {
        public Account()
        {
            AccountFee = new HashSet<AccountFee>();
            CashFlowCashFromNavigation = new HashSet<CashFlow>();
            CashFlowCashToNavigation = new HashSet<CashFlow>();
            InverseParent = new HashSet<Account>();
            OrganisationAccounts = new HashSet<OrganisationAccount>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal PerTransMin { get; set; }
        public decimal PerTransMax { get; set; }
        public decimal? MonthlyLimit { get; set; }
        public decimal? MonthlySum { get; set; }
        public bool? State { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Account Parent { get; set; }
        public virtual ICollection<AccountFee> AccountFee { get; set; }
        public virtual ICollection<CashFlow> CashFlowCashFromNavigation { get; set; }
        public virtual ICollection<CashFlow> CashFlowCashToNavigation { get; set; }
        public virtual ICollection<Account> InverseParent { get; set; }
        public virtual ICollection<OrganisationAccount> OrganisationAccounts { get; set; }
    }
}
