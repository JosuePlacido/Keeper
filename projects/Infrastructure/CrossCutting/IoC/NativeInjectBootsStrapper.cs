using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Application.Contract.DAL;
using AutoMapper;
using Infrastructure.CrossCutting.Adapter;
using Application.Services.CreateChampionship;
using Application.Services.EditChampionship;
using Application.Services.MatchService;
using Application.Services.RegisterResult;
using Domain.Rules.TiebreakCriterion;
using Application.Services.CRUDTeam;
using Application.Services.CRUDPlayer;
using Domain.Provider;
using Application.Adapter;

namespace Infrastructure.CrossCutting.IoC
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
			services.AddScoped<IRegisterResultService, RegisterResultService>();
			services.AddScoped<IEditChampionshipService, EditChampionshipService>();
			services.AddScoped<IChampionshipService, ChampionshipService>();
		}
	}
}
