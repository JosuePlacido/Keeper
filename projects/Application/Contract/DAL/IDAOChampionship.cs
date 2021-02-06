using System.Threading.Tasks;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Application.Contract
{
	public interface IDAOChampionship : IDAO
	{
		Task<ObjectRenameDTO> GetByIdForRename(string id);
	}
}
