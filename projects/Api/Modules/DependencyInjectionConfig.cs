using System;
using Keeper.Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Keeper.Api.Modules
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
