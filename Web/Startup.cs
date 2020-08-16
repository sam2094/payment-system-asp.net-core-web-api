using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Settings;
using Web.Installers;
using Microsoft.AspNetCore.HttpOverrides;

namespace Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) => Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
            services.InstallServicesAssembly(Configuration);
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			// Ip Address
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor |
			ForwardedHeaders.XForwardedProto
			});

			// Common configuration
		//	app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			// Swagger configuration
			var swaggerSettings = new SwaggerSettings();
			Configuration.GetSection(nameof(SwaggerSettings)).Bind(swaggerSettings);
			app.UseSwagger(option =>
			{
				option.RouteTemplate = swaggerSettings.JsonRoute;
			});

			app.UseSwaggerUI(option =>
			{
				option.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description);
			});

			// Auth configuration
			app.UseCors(x => x
				 .AllowAnyOrigin()
				 .AllowAnyMethod()
				 .AllowAnyHeader());

			// Auth configuration
			app.UseAuthentication();
			app.UseAuthorization();

			// Endpoint configuration
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
