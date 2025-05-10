using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

namespace Api.Modules
{
	public static class SQLServerExtensions
	{
		public static IServiceCollection AddSQLServer(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			// IFeatureManager featureManager = services
			// 	.BuildServiceProvider()
			// 	.GetRequiredService<IFeatureManager>();

			// bool isEnabled = featureManager
			// 	.IsEnabledAsync(nameof(CustomFeature.SQLServer))
			// 	.ConfigureAwait(false)
			// 	.GetAwaiter()
			// 	.GetResult();
			// Console.WriteLine($"{isEnabled} - {nameof(CustomFeature.SQLServer)}");
			// if (isEnabled)
			// {
			services.AddDbContext<ApplicationContext>(
				options => options.UseSqlServer(
					configuration.GetValue<string>("PersistenceModule:DefaultConnection")));
			//services.AddScoped<IUnitOfWork, UnitOfWork>();

			//services.AddScoped<IAccountRepository, AccountRepository>();
			// }
			// // else
			// {
			//	services.AddSingleton<MangaContextFake, MangaContextFake>();
			//	services.AddScoped<IUnitOfWork, UnitOfWorkFake>();
			//	services.AddScoped<IAccountRepository, AccountRepositoryFake>();
			// }

			//services.AddScoped<IAccountFactory, EntityFactory>();

			return services;
		}
	}
}
