using Api.Modules;
using Api.Modules.FeatureFlags;
using Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) =>
			services
				.AddFeatureFlags(this.Configuration) // should be the first one.
													 //.AddInvalidRequestLogging()
													 //.AddCurrencyExchange(this.Configuration)
				.AddSQLServer(this.Configuration)
				//.AddSqlLite(this.Configuration)
				.AddMediatRConfiguration()
				//.AddHealthChecks(this.Configuration)
				//.AddAuthentication(this.Configuration)
				//.AddVersioning()
				//.AddSwagger()
				.AddGlobalExceptionHandlerMiddleware()
				.AddDependencyInjectionConfiguration()
				//.AddCustomCors()
				//.AddProxy();
				//.AddCustomDataProtection();/*
				;
		/*{
			services.Configure<ConfigurationManager>(Configuration.GetSection("ConfigurationManager"));
			services.AddSQLServer(this.Configuration);
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
			});
		}*/

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env
			, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseGlobalExceptionHandlerMiddleware();
			app
				//.UseProxy(this.Configuration)
				//.UseHealthChecks()
				//.UseCustomCors()
				//.UseCustomHttpMetrics()
				.UseRouting()
				//.UseVersionedSwagger(provider, this.Configuration, env)
				//.UseAuthentication()
				//.UseAuthorization()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
					//endpoints.MapMetrics();
				});
		}
	}
}
