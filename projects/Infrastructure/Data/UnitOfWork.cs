using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Application.DAO;
using Keeper.Domain.Core;
using Keeper.Domain.Models;
using Keeper.Domain.Repository;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Keeper.Infrastructure.Repository;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ApplicationContext _context;
		private readonly IDictionary<Type, Func<ApplicationContext, object>> _daos =
			new Dictionary<Type, Func<ApplicationContext, object>>
			{
				[typeof(IDAOChampionship)] = (ctx) => new DAOChampionship(ctx),
				[typeof(IDAOTeam)] = (ctx) => new DAOTeam(ctx),
				[typeof(IDAOPlayer)] = (ctx) => new DAOPlayer(ctx),
				[typeof(IDAOPlayerSubscribe)] = (ctx) => new DAOPlayerSubscribe(ctx),
				[typeof(IDAOStatistic)] = (ctx) => new DAOStatistic(ctx),
				[typeof(IDAOTeamSubscribe)] = (ctx) => new DAOTeamSubscribe(ctx),
				[typeof(IRepositoryChampionship)] = (ctx) => new ChampionshipRepository(ctx),
				[typeof(IRepositoryTeam)] = (ctx) => new TeamRepository(ctx),
				[typeof(IRepositoryPlayer)] = (ctx) => new PlayerRepository(ctx),
			};
		public UnitOfWork(ApplicationContext context)
		{
			_context = context;
		}
		public async Task Commit()
		{
			await _context.SaveChangesAsync();
		}
		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public object GetDAO(Type type)
		{
			return _daos[type](_context);
		}
	}
}
