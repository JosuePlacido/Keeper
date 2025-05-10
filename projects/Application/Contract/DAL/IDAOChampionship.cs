using System.Threading.Tasks;
using Application.Services.EditChampionship;

namespace Application.Contract.DAL
{
	public interface IDAOChampionship : IDAO
	{
		Task<ObjectRenameDTO> GetByIdForRename(string id);
	}
}
