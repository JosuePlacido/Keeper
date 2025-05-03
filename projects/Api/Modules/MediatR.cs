using Api;
using Application.EventHandler;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Modules;
public static class MediatRExtension
{
	public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
	{
		services.AddMediatR(typeof(Startup));

		services.AddScoped<INotificationHandler<PersistStatisticsMatchResultEvent>,
			PersistMatchResultDomainEventHandler>();
		services.AddScoped<INotificationHandler<AdvanceChampionshipEvent>,
			AdvanceChampionshipDomainEventHandler>();
		services.AddScoped<INotificationHandler<RankingEvent>,
			RankingEventHandler>();
		return services;
	}
}
