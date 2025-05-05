using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace Application.Contract.DAL;
public interface IDAOPlayer : IDAO
{
	Task<bool> IsDeletable(string id);
	Task<string[]> Exists(string[] vs);
	Task<PaginationDTO<Player>> GetAvailables(string terms, string championship, int page, int take);
	Task<PaginationDTO<Player>> List(string terms, int page, int take);
}

