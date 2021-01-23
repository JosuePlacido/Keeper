using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Infrastructure.DAO
{
	public interface IDAOTeam : IDAO<Team>
	{
		Task<TeamViewDTO> GetByIdView(string id);
	}
}
