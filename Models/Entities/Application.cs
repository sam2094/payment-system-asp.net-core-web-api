using Common.Enums.DatabaseEnums;
using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Application : BaseEntity
    {
        public Application()
        {
            Invoices = new HashSet<Invoice>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrgId { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool State { get; set; }
        public int? FailLimit { get; set; }
        public int AppTypeId { get; set; }
        public string DependIp { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Updated { get; set; }

        public virtual AppType AppType { get; set; }
        public virtual Organisation Org { get; set; }
        public virtual MerchantSetting MerchantSettings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
