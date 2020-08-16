using Common.Enums.DatabaseEnums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entities;
using System;

namespace DataAccess.Database
{
	public class ModelConfiguration
	{
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AccountFee>(entity =>
			{
				entity.HasKey(e => new { e.OrgId, e.CurrencyId, e.AccountId, e.AccountTypeId });

				entity.ToTable("AccountFee", "Finance");

				entity.Property(e => e.Fee).HasColumnType("decimal(18, 0)");

				entity.HasOne(d => d.Account)
					.WithMany(p => p.AccountFee)
					.HasForeignKey(d => d.AccountId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AccountFee_Accounts");

				entity.HasOne(d => d.AccountType)
					.WithMany(p => p.AccountFee)
					.HasForeignKey(d => d.AccountTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AccountFee_AccountType");

				entity.HasOne(d => d.Currency)
					.WithMany(p => p.AccountFee)
					.HasForeignKey(d => d.CurrencyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AccountFee_Currency");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.AccountFee)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_AccountFee_Organisations");
			});

			modelBuilder.Entity<AccountType>(entity =>
			{
				entity.ToTable("AccountType", "Account");

				entity.Property(e => e.Description).HasMaxLength(250);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Symbol)
					.IsRequired()
					.HasMaxLength(5);
			});

			modelBuilder.Entity<ForgetPasswords>(entity =>
			{
				entity.ToTable("ForgetPassword", "Security");

				entity.Property(e => e.UserId).IsRequired();

				entity.Property(e => e.Code).IsRequired();

				entity.Property(e => e.CreatedDate)
					 .HasColumnType("datetime")
					 .HasDefaultValueSql("(getdate())"); ;
			});

			modelBuilder.Entity<Account>(entity =>
			{
				entity.ToTable("Accounts", "Account");

				entity.Property(e => e.Balance).HasColumnType("money");

				entity.Property(e => e.Created).HasColumnType("datetime");

				entity.Property(e => e.MonthlyLimit).HasColumnType("money");

				entity.Property(e => e.MonthlySum).HasColumnType("money");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.PerTransMax).HasColumnType("money");

				entity.Property(e => e.PerTransMin).HasColumnType("money");

				entity.Property(e => e.State)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.Parent)
					.WithMany(p => p.InverseParent)
					.HasForeignKey(d => d.ParentId)
					.HasConstraintName("FK_Accounts_Accounts");
			});

			modelBuilder.Entity<ActivityScope>(entity =>
			{
				entity.HasKey(e => new { e.OrgId, e.ScopeId });

				entity.ToTable("ActivityScope", "Org");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.ActivityScope)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ActivityScope_Organisations");

				entity.HasOne(d => d.Scope)
					.WithMany(p => p.ActivityScope)
					.HasForeignKey(d => d.ScopeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_ActivityScope_OrganiztaionScope");
			});

			modelBuilder.Entity<AppType>(entity =>
			{
				entity.ToTable("AppType", "App");

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<Application>(entity =>
			{
				entity.ToTable("Applications", "App");

				entity.Property(e => e.Added)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.DependIp)
					.HasColumnName("DependIP")
					.HasMaxLength(20);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				var converter = new BoolToStringConverter(ApplicationTypes.INVOICE.ToString(), ApplicationTypes.PAYMENT.ToString());

				// convert bool to enum
				var converter2 = new ValueConverter<ApplicationTypes, bool>(v => v.ToString() == ApplicationTypes.INVOICE.ToString() ? false : true, v => v == false ? (ApplicationTypes)Enum.Parse(typeof(ApplicationTypes), ApplicationTypes.INVOICE.ToString()) : (ApplicationTypes)Enum.Parse(typeof(ApplicationTypes), ApplicationTypes.PAYMENT.ToString()));

				entity.Property(e => e.ApplicationType)
					.IsRequired()
					.HasConversion(converter2);

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.AppType)
					.WithMany(p => p.Applications)
					.HasForeignKey(d => d.AppTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Applications_AppType");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.Applications)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Applications_Organisations");
			});

			modelBuilder.Entity<Branch>(entity =>
			{
				entity.ToTable("Branches", "Org");

				entity.Property(e => e.Added).HasColumnType("datetime");

				entity.Property(e => e.Adress).HasMaxLength(200);

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Latitude).HasColumnType("decimal(18, 12)");

				entity.Property(e => e.Longitude).HasColumnType("decimal(18, 12)");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.State)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.Branches)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Branches_Organisations");
			});

			modelBuilder.Entity<CashFlow>(entity =>
			{
				entity.ToTable("CashFlow", "Finance");

				entity.Property(e => e.Date).HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.SumFrom).HasColumnType("money");

				entity.Property(e => e.SumTo).HasColumnType("money");

				entity.HasOne(d => d.CashFromNavigation)
					.WithMany(p => p.CashFlowCashFromNavigation)
					.HasForeignKey(d => d.CashFrom)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CashFlow_Accounts");

				entity.HasOne(d => d.CashToNavigation)
					.WithMany(p => p.CashFlowCashToNavigation)
					.HasForeignKey(d => d.CashTo)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CashFlow_Accounts1");

				entity.HasOne(d => d.Pair)
					.WithMany(p => p.CashFlow)
					.HasForeignKey(d => d.PairId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CashFlow_CurrencyPairs");
			});

			modelBuilder.Entity<City>(entity =>
			{

				entity.ToTable("Cities", "Geo");

				entity.Property(e => e.DigitCode).HasMaxLength(20);

				entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");

				entity.Property(e => e.Longtitude).HasColumnType("decimal(18, 0)");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.Region)
				  .WithMany()
				  .HasForeignKey(d => d.RegionId)
				  .OnDelete(DeleteBehavior.NoAction)
				  .HasConstraintName("FK_Cities_Region");
			});

			modelBuilder.Entity<Country>(entity =>
			{

				entity.ToTable("Country", "Geo");

				entity.Property(e => e.Isocode)
					.HasColumnName("ISOCode")
					.HasMaxLength(20);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.PhoneCode)
					.IsRequired()
					.HasMaxLength(10);
			});

			modelBuilder.Entity<Currency>(entity =>
			{
				entity.ToTable("Currency", "Finance");

				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.CharSign)
					.IsRequired()
					.HasMaxLength(5);

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(20);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<CurrencyPair>(entity =>
			{
				entity.ToTable("CurrencyPairs", "Finance");

				entity.Property(e => e.State)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.HasOne(d => d.BaseCurrencyNavigation)
					.WithMany(p => p.CurrencyPairsBaseCurrencyNavigation)
					.HasForeignKey(d => d.BaseCurrency)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CurrencyPairs_Currency");

				entity.HasOne(d => d.SecondaryCurrencyNavigation)
					.WithMany(p => p.CurrencyPairsSecondaryCurrencyNavigation)
					.HasForeignKey(d => d.SecondaryCurrency)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CurrencyPairs_Currency1");
			});

			modelBuilder.Entity<CurrencyRate>(entity =>
			{
				entity.ToTable("CurrencyRates", "Finance");

				entity.Property(e => e.Added)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Day).HasColumnType("date");

				entity.Property(e => e.Nominal).HasColumnType("money");

				entity.Property(e => e.Rate).HasColumnType("money");

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.Pair)
					.WithMany(p => p.CurrencyRates)
					.HasForeignKey(d => d.PairId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_CurrencyRates_CurrencyPairs");
			});

			modelBuilder.Entity<InvoiceState>(entity =>
			{
				entity.ToTable("InvoiceStates", "Opr");

				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<Invoice>(entity =>
			{
				entity.ToTable("Invoices", "Opr");

				entity.Property(e => e.Email).HasMaxLength(100);

				entity.Property(e => e.ExpDate).HasColumnType("datetime");

				entity.Property(e => e.Info).HasMaxLength(500);

				entity.Property(e => e.Ip).HasMaxLength(20);

				entity.Property(e => e.OrderId)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.SenderDate).HasColumnType("datetime");

				entity.Property(e => e.UpdateDate).HasColumnType("datetime")
				 .HasDefaultValueSql("(getdate())");

				entity.Property(e => e.ServerDate)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Sum).HasColumnType("money");

				entity.Property(e => e.RemainderSum).HasColumnType("money");

				entity.Property(e => e.Value)
					.IsRequired()
					.HasMaxLength(500);

				entity.HasOne(d => d.App)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.AppId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_Applications");

				entity.HasOne(d => d.Branch)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.BranchId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_Branches");

				entity.HasOne(d => d.Currency)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.CurrencyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_Currency");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_Organisations");

				entity.HasOne(d => d.State)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.StateId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_InvoiceStates");

				entity.HasOne(d => d.User)
					.WithMany(p => p.Invoices)
					.HasForeignKey(d => d.UserId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Invoices_Users");
			});

			modelBuilder.Entity<Key>(entity =>
			{
				entity.HasNoKey();

				entity.ToTable("Keys", "App");

				entity.Property(e => e.AppKey)
					.IsRequired()
					.HasMaxLength(250);
			});

			modelBuilder.Entity<MerchantSetting>(entity =>
			{
				entity.ToTable("MerchantSettings", "App");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.EncKey).HasMaxLength(250);

				entity.Property(e => e.ErrorUri)
					.HasMaxLength(500);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.SuccesUri)
					.HasMaxLength(500);

				entity.HasOne(d => d.IdNavigation)
					.WithOne(p => p.MerchantSettings)
					.HasForeignKey<MerchantSetting>(d => d.Id)
					.OnDelete(DeleteBehavior.Cascade)
					.HasConstraintName("FK_MerchantSettings_Applications");
			});

			modelBuilder.Entity<OrgTypes>(entity =>
			{
				entity.ToTable("OrgTypes", "Org");

				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<OrganisationAccount>(entity =>
			{
				entity.HasKey(e => new { e.OrgId, e.AccountId, e.AccountTypeId, e.CurrencyId });

				entity.HasOne(d => d.Account)
					.WithMany(p => p.OrganisationAccounts)
					.HasForeignKey(d => d.AccountId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_OrganisationAccounts_Accounts");

				entity.HasOne(d => d.AccountType)
					.WithMany(p => p.OrganisationAccounts)
					.HasForeignKey(d => d.AccountTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_OrganisationAccounts_AccountType");

				entity.HasOne(d => d.Currency)
					.WithMany(p => p.OrganisationAccounts)
					.HasForeignKey(d => d.CurrencyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_OrganisationAccounts_Currency");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.OrganisationAccounts)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_OrganisationAccounts_Organisations");
			});

			modelBuilder.Entity<OrganisationDetail>(entity =>
			{
				entity.ToTable("OrganisationDetails", "Org");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Kppcode)
					.HasColumnName("KPPCode")
					.HasMaxLength(50);

				entity.Property(e => e.OrgName)
					.IsRequired()
					.HasMaxLength(500);

				entity.Property(e => e.Sun)
					.IsRequired()
					.HasColumnName("SUN")
					.HasMaxLength(50);

				entity.Property(e => e.Voen)
					.IsRequired()
					.HasColumnName("VOEN")
					.HasMaxLength(50);
			});

			modelBuilder.Entity<Organisation>(entity =>
			{
				entity.ToTable("Organisations", "Org");

				entity.Property(e => e.Added).HasColumnType("datetime");

				entity.Property(e => e.Address).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.OrgTypeNavigation)
					.WithMany(p => p.Organisations)
					.HasForeignKey(d => d.OrgType)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Organisations_OrgTypes");

				entity.HasOne(d => d.OrganisationDetails)
				  .WithMany()
				  .HasForeignKey(d => d.DetailsId)
				  .OnDelete(DeleteBehavior.Cascade)
				  .HasConstraintName("FK_Organisations_OrganisationDetails");
			});

			modelBuilder.Entity<OrganiztaionScope>(entity =>
			{
				entity.ToTable("OrganiztaionScope", "Org");

				entity.Property(e => e.Description)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<PaymentState>(entity =>
			{
				entity.ToTable("PaymentState", "Opr");

				entity.Property(e => e.Description).HasMaxLength(500);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<Payment>(entity =>
			{
				entity.ToTable("Payments", "Opr");

				entity.Property(e => e.CheckNumb).HasMaxLength(50);

				entity.Property(e => e.Description).HasMaxLength(500);

				entity.Property(e => e.ExternalDate).HasColumnType("datetime");

				entity.Property(e => e.ExternalPoint).HasMaxLength(50);

				entity.Property(e => e.Sum).HasColumnType("money");

				entity.Property(e => e.SystemDate)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.UpdateDate)
				   .HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.TransId)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.App)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.AppId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_Applications");

				entity.HasOne(d => d.CashFlow)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.CashFlowId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_CashFlow");

				entity.HasOne(d => d.Currency)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.CurrencyId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_Currency");

				entity.HasOne(d => d.Invoice)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.InvoiceId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_Invoices");

				entity.HasOne(d => d.Org)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.OrgId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_Organisations");

				entity.HasOne(d => d.Parent)
					.WithMany(p => p.InverseParent)
					.HasForeignKey(d => d.ParentId)
					.HasConstraintName("FK_Payments_Payments");

				entity.HasOne(d => d.State)
					.WithMany(p => p.Payments)
					.HasForeignKey(d => d.StateId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Payments_PaymentState");
			});

			modelBuilder.Entity<Permission>(entity =>
			{
				entity.ToTable("Permission", "Security");

				entity.Property(e => e.Added)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(20);

				entity.Property(e => e.Description).HasMaxLength(100);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.Group)
					.WithMany(p => p.Permission)
					.HasForeignKey(d => d.GroupId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Permission_PermissionGroup");
			});

			modelBuilder.Entity<PermissionActionType>(entity =>
			{
				entity.ToTable("PermissionActionType", "Security");

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(10);

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<PermissionGroup>(entity =>
			{
				entity.ToTable("PermissionGroup", "Security");

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(20);

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(20);
			});

			modelBuilder.Entity<Region>(entity =>
			{
				entity.ToTable("Region", "Geo");

				entity.Property(e => e.Description)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.DigitCode).HasMaxLength(20);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.HasOne(d => d.Country)
				   .WithMany()
				   .HasForeignKey(d => d.CountryId)
				   .OnDelete(DeleteBehavior.NoAction)
				   .HasConstraintName("FK_Region_Country");
			});

			modelBuilder.Entity<Role>(entity =>
			{
				entity.ToTable("Role", "Security");

				entity.Property(e => e.Code)
					.IsRequired()
					.HasMaxLength(20);

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);
			});

			modelBuilder.Entity<RolePermission>(entity =>
			{
				entity.HasKey(e => new { e.RoleId, e.ActionTypeId, e.PermissionId });

				entity.ToTable("RolePermission", "Security");

				entity.HasOne(d => d.ActionType)
					.WithMany(p => p.RolePermission)
					.HasForeignKey(d => d.ActionTypeId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_RolePermission_PermissionActionType");

				entity.HasOne(d => d.Permission)
					.WithMany(p => p.RolePermission)
					.HasForeignKey(d => d.PermissionId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_RolePermission_Permission");

				entity.HasOne(d => d.Role)
					.WithMany(p => p.RolePermission)
					.HasForeignKey(d => d.RoleId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_RolePermission_Role");
			});

			modelBuilder.Entity<Timezone>(entity =>
			{
				entity.HasNoKey();

				entity.ToTable("Timezones", "Geo");

				entity.Property(e => e.Description).HasMaxLength(200);

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Prev).HasMaxLength(100);
			});

			modelBuilder.Entity<UserToken>(entity =>
			{
				entity.HasKey(e => new { e.Userid, e.Token });

				entity.Property(e => e.Token).HasMaxLength(255);

				entity.Property(e => e.GenerationDate)
					.HasColumnType("datetime")
					.HasDefaultValueSql("(getdate())");

				entity.Property(e => e.LifePeriod).HasColumnType("decimal(18, 2)");

				entity.HasOne(d => d.User)
					.WithMany(p => p.UserTokens)
					.HasForeignKey(d => d.Userid)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_UserTokens_Users");
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("Users", "Org");

				entity.Property(e => e.Added).HasColumnType("datetime");

				entity.Property(e => e.FirstName)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.LastAuthDate).HasColumnType("datetime");

				entity.Property(e => e.LastAuthIp).HasMaxLength(20);

				entity.Property(e => e.LastName)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Email)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.Login)
					.IsRequired()
					.HasMaxLength(50);

				entity.Property(e => e.PasswordHash)
					.IsRequired()
					.HasMaxLength(500);

				entity.Property(e => e.PasswordSalt)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.Patronomic).HasMaxLength(50);

				entity.Property(e => e.Phone).HasMaxLength(50);

				entity.Property(e => e.State)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.Updated).HasColumnType("datetime");

				entity.HasOne(d => d.Branch)
					.WithMany(p => p.Users)
					.HasForeignKey(d => d.BranchId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Users_Branches");

				entity.HasOne(d => d.Role)
					.WithMany(p => p.Users)
					.HasForeignKey(d => d.RoleId)
					.OnDelete(DeleteBehavior.ClientSetNull)
					.HasConstraintName("FK_Users_Role");
			});
		}
	}
}
