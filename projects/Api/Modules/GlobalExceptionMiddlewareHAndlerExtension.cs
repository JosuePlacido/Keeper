
using Keeper.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Keeper.Api.Extensions
{
	public static class GlobalExceptionHandlerMiddlewareExtensions
	{
		public static IServiceCollection AddGlobalExceptionHandlerMiddleware(this IServiceCollection services)
		{
			return services.AddTransient<GlobalExceptionHandlerMiddleware>();
		}

		public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
		}
	}
}
