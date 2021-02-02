using Microsoft.Extensions.DependencyInjection;
using Keeper.Application.Services;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.Data;
using Keeper.Application.Interface;
using Keeper.Domain.Repository;
using AutoMapper;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Infrastructure.DAO;
using Keeper.Application.DAO;
using Domain.Core;
using Infrastructure.Data;

namespace Keeper.Infrastructure.CrossCutting.IoC
{
	public static class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ChampionshipDTOToDomainProfile), typeof(TeamDTOProfile),
				typeof(MatchEditProfile), typeof(PlayerDTOProfile));

			services.AddScoped<ApplicationContext>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IRepositoryChampionship, ChampionshipRepository>();
			services.AddScoped<IRepositoryTeam, TeamRepository>();

			services.AddScoped<IDAOTeam, DAOTeam>();
			services.AddScoped<IRepositoryPlayer, PlayerRepository>();
			services.AddScoped<IDAOPlayer, DAOPlayer>();
			services.AddScoped<IDAOChampionship, DAOChampionship>();

			services.AddScoped<ITeamService, TeamService>();
			services.AddScoped<IDAOPlayerSubscribe, DAOPlayerSubscribe>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IChampionshipService, ChampionshipService>();
		}
	}
}
