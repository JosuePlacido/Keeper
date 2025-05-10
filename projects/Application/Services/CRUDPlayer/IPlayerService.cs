using System;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTO;

namespace Application.Services.CRUDPlayer;
public interface IPlayerService : IDisposable
{
	Task<Player> Create(PlayerCreateDTO dto);
	Task<Player> Delete(string id);
	Task<Player> Get(string id);
	Task<PaginationDTO<Player>> GetAvailables(string terms, string championship, int page, int take);
	Task<Player> Update(PlayerUpdateDTO dto);
	Task<PaginationDTO<Player>> List(string terms, int page, int take);
}
