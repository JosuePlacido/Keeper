using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Domain.Models;

namespace Keeper.Application.DAO
{
	public interface IDAOPlayerSubscribe
	{
		Task<string> ValidateUpdateOnSquad(PlayerSubscribe player);
	}
}
