using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Parameters.Payments;
using Models.Parameters.User;
using Services.Services.AuthServices;
using Services.Services.PaymentService;
using Services.Services.PaymentService.Consolidate;
using Services.Services.UserServices;

namespace Web.Installers
{
	public class ServiceInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
            services.AddTransient<IUserService<AuthorizationOutput>, UserService>();
            services.AddTransient<IVerifyTokenService<VerifyTokenOutput>, VerifyTokenService>();
            services.AddTransient<IPaymentService<PaymentOutput>, PaymentService>();
            services.AddTransient<IPaymentStatusService<PaymentOutput>, PaymentStatusService>();
            services.AddTransient<IConsolidateService<ConsolidateOutput>, ConsolidateService>();
            services.AddTransient<ICancellPaymentService<PaymentOutput>, CancellPaymentService>();
            services.AddTransient<IGetBalanceService<GetBalanceOutput>, GetBalanceService>();
            services.AddTransient<IAuthService, AuthService> ();
        }
	}
}
