using Keeper.Domain.Models;

namespace Keeper.Infrastructure.DAO
{
	public interface IDAOChampionship
	{
		string[] VerifyCreatedIds(Championship championship);
	}
}
