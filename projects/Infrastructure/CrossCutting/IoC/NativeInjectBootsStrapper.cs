using Microsoft.Extensions.DependencyInjection;
using Keeper.Application.Services;
using Keeper.Infrastructure.Data;
using Keeper.Application.Contract;
using AutoMapper;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Application.Services.CreateChampionship;
using Keeper.Application.Services.EditChampionship;
using Keeper.Application.Services.MatchService;

namespace Keeper.Infrastructure.CrossCutting.IoC
{
	public static class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ChampionshipDTOToDomainProfile),
				typeof(TeamDTOProfile), typeof(MatchEditProfile),
				typeof(PlayerDTOProfile), typeof(RankDTOProfile),
				typeof(SingleStatisticDTOProfile));

			services.AddScoped<ApplicationContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddScoped<ITeamService, TeamService>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IMatchService, MatchService>();
			services.AddScoped<IEditChampionshipService, EditChampionshipService>();
			services.AddScoped<IChampionshipService, ChampionshipService>();
		}
	}
}
