using Api;
using Application.EventHandler;
using Application.Services.RegisterResult;
using Domain.Events;
using MediatR;
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
		services.AddScoped<INotificationHandler<RankingEvent>,
			RankingEventHandler>();
		return services;
	}
}
