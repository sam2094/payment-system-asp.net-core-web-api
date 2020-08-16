using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Currency : BaseEntity
    {
        public Currency()
        {
            AccountFee = new HashSet<AccountFee>();
            CurrencyPairsBaseCurrencyNavigation = new HashSet<CurrencyPair>();
            CurrencyPairsSecondaryCurrencyNavigation = new HashSet<CurrencyPair>();
            Invoices = new HashSet<Invoice>();
            OrganisationAccounts = new HashSet<OrganisationAccount>();
            Payments = new HashSet<Payment>();
        }

        public byte Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CharSign { get; set; }
        public int DigitCode { get; set; }
        public bool State { get; set; }

        public virtual ICollection<AccountFee> AccountFee { get; set; }
        public virtual ICollection<CurrencyPair> CurrencyPairsBaseCurrencyNavigation { get; set; }
        public virtual ICollection<CurrencyPair> CurrencyPairsSecondaryCurrencyNavigation { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<OrganisationAccount> OrganisationAccounts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
