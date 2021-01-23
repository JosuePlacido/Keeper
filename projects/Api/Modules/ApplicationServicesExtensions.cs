using Microsoft.Extensions.DependencyInjection;
using Keeper.Application.Interface;
using Keeper.Application.Services;
namespace Keeper.Api.Modules
{

	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<ITeamService, TeamService>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IChampionshipService, ChampionshipService>();

			return services;
		}
	}
}
