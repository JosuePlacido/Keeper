using System;
using Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Modules
{
	public static class DependencyInjectionConfig
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			NativeInjectorBootStrapper.RegisterServices(services);
		}
	}
}
