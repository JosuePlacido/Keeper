using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace Application.Contract.DAL;
public interface IDAOTeam : IDAO
{
	Task<string[]> Exists(string[] ids);
	Task<PaginationDTO<Team>> GetsNotInChampionship(string terms, string championshipId, int page, int take);
	Task<PaginationDTO<Team>> List(string terms, int page, int take);
	Task<(Team team, bool isDeletable)> GetByIdDeletable(string id);
}
