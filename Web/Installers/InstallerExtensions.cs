using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Web.Installers
{
	public static class InstallerExtensions
	{
		public static void InstallServicesAssembly(this IServiceCollection services, IConfiguration configuration)
		{
			var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
			typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

			installers.ForEach(installer => installer.InstallServices(services, configuration));
		}
	}
}
