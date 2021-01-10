using System.Threading.Tasks;
using AutoMapper;
using Domain.Repository;
using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Repository
{
	public class ChampionshipRepository : IRepositoryChampionship
	{
		private readonly ApplicationContext _context;
		private readonly DAOChampionship _dao;
		public ChampionshipRepository(ApplicationContext Context)
		{
			_context = Context;
			_dao = new DAOChampionship(Context);
		}
		public void Add(Championship obj)
		{
			_dao.Add(obj);
		}

		public void Dispose()
		{
			_dao.Dispose();
		}

		public Task<Championship[]> GetAll()
		{
			throw new System.NotImplementedException();
		}

		public Task<Championship> GetById(string id)
		{
			throw new System.NotImplementedException();
		}

		public void Remove(Championship obj)
		{
			throw new System.NotImplementedException();
		}

		public void Update(Championship obj)
		{
			throw new System.NotImplementedException();
		}

		public string[] VerifyCreatedIds(Championship championship)
		{
			var invalids = new List<string>();
			var teams = championship.Teams.Select(ts => ts.Id).ToArray();
			var players = championship.Teams.SelectMany(ts => ts.Players)
				.Select(ts => ts.Id).ToArray();
			var groups = championship.Stages.SelectMany(stage => stage.Groups)
				.Select(group => group.Id).ToArray();
			var vacancys = championship.Stages.SelectMany(stage => stage.Groups)
				.SelectMany(group => group.Vacancys).Select(vacancy => vacancy.Id).ToArray();
			invalids.AddRange(new DAOGroup(_context).VerifyId(groups));
			invalids.AddRange(new DAOVacancy(_context).VerifyId(vacancys));
			invalids.AddRange(new DAOPlayerSubscribe(_context).VerifyId(players));
			invalids.AddRange(new DAOTeamSubscribe(_context).VerifyId(teams));
			return invalids.ToArray();
		}
	}
}
