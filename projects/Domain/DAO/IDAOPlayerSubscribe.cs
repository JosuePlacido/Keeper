using System.Threading.Tasks;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOPlayerSubscribe
	{
		Task<string> ValidateUpdateOnSquad(PlayerSubscribe player);
	}
}
