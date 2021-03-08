using System.Reflection;
using Keeper.Api;
using Keeper.Application.Services.RegisterResult;
using Keeper.Domain.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class MEdiatRExtension
{
	public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
	{
		services.AddMediatR(typeof(Startup));

		services.AddScoped<INotificationHandler<RegisterResultEvent>,
			RegisterResultDomainEventHandler>();
		services.AddScoped<INotificationHandler<UpdateChampionshipEvent>,
			UpdateChampionshipDomainEventHandler>();
		return services;
	}
}
