using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
namespace Api.Modules.FeatureFlags
{
	public static class FeatureFlagsExtensions
	{
		public static IServiceCollection AddFeatureFlags(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddFeatureManagement(configuration);

			IFeatureManager featureManager = services.BuildServiceProvider()
				.GetRequiredService<IFeatureManager>();

			services.AddMvc()
				.ConfigureApplicationPartManager(apm =>
					apm.FeatureProviders.Add(
						new CustomControllerFeatureProvider(featureManager)));
			return services;
		}
	}
}
