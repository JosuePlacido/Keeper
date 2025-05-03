using System;
using System.Threading.Tasks;
using Domain.Models;
using Application.Contract;

namespace Application.Services.CRUDPlayer
{
	public interface IPlayerService : IDisposable
	{
		Task<Player> Create(PlayerCreateDTO dto);
		Task<IServiceResponse> Delete(string dto);
		Task<Player> Get(string id);
		Task<PlayerAvailablePaginationDTO> GetAvailables(string terms, string championship, int page, int take);
		Task<IServiceResponse> Update(PlayerUpdateDTO dto);
		Task<Player[]> Get();
	}
}
