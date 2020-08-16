using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Database
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountFee> AccountFee { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ActivityScope> ActivityScope { get; set; }
        public virtual DbSet<AppType> AppType { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CashFlow> CashFlow { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CurrencyPair> CurrencyPairs { get; set; }
        public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }
        public virtual DbSet<InvoiceState> InvoiceStates { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Key> Keys { get; set; }
        public virtual DbSet<MerchantSetting> MerchantSettings { get; set; }
        public virtual DbSet<OrgTypes> OrgTypes { get; set; }
        public virtual DbSet<OrganisationAccount> OrganisationAccounts { get; set; }
        public virtual DbSet<OrganisationDetail> OrganisationDetails { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<OrganiztaionScope> OrganiztaionScope { get; set; }
        public virtual DbSet<PaymentState> PaymentState { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionActionType> PermissionActionType { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroup { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Timezone> Timezones { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ForgetPasswords> ForgetPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server = .\\SQLEXPRESS; database=Payment;Initial Catalog=Payment;Persist Security Info=true;;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
