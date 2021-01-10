using Domain.Repository;
using Domain.Models;

namespace Domain.Repository
{
	public interface IRepositoryChampionship : IRepositoryBase<Championship>
	{
		string[] VerifyCreatedIds(Championship championship);
	}
}
