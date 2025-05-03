using Application.Contract.DAL;
using Application.Contract.Repository;
using Domain.Models;
using Domain.Provider;

namespace Application.Adapter
{

	public class MatchDataProvider : IMatchDataProvider
	{
		private readonly IUnitOfWork _uow;

		public MatchDataProvider(IUnitOfWork uow) => _uow = uow;

		public Match[] GetMatchsByGroupWithTeams(string group, string[] teamSubscribes)
		{
			IRepositoryMatch repository = (IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch));
			return repository.GetByGroupAndTeams(group, teamSubscribes).Result;
		}

		public bool HasMatchsPendingInGroup(string[] groupsId)
		{
			IRepositoryMatch repository = (IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch));
			Match[] matches = repository.GetAllMatchesPendingInGroups(groupsId).Result;
			return matches.Length > 0;
		}
	}

}