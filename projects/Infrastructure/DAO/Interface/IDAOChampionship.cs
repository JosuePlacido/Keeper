using Domain.Models;

namespace Infrastructure.DAO.Interface
{
	public interface IDAOChampionship
	{
		string[] VerifyCreatedIds(Championship championship);
	}
}
