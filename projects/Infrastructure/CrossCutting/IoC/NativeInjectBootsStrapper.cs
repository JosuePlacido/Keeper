using Microsoft.Extensions.DependencyInjection;
//using Keeper.Application.Services;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.Data;
//using Keeper.Application.Interface;
using Keeper.Domain.Repository;
using AutoMapper;
using System;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Infrastructure.DAO;
namespace Keeper.Infrastructure.CrossCutting.IoC
{
	public static class NativeInjectorBootStrapper
	{
		public static void RegisterServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ChampionshipDTOToDomainProfile), typeof(TeamDTOProfile),
				typeof(MatchEditProfile));

			services.AddScoped<ApplicationContext>();
			services.AddScoped<IRepositoryChampionship, ChampionshipRepository>();
			services.AddScoped<IRepositoryTeam, TeamRepository>();
			services.AddScoped<IDAOTeam, DAOTeam>();
		}
	}
}
