using System;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;

namespace Keeper.Application.Interface
{
	public interface IPlayerService : IDisposable
	{
		Task<Player> Create(PlayerCreateDTO dto);
		Task<IServiceResult> Delete(string dto);
		Task<Player> Get(string id);
		Task<PlayerAvailablePaginationDTO> GetAvailables(string terms, string championship, int page, int take);
		Task<IServiceResult> Update(PlayerUpdateDTO dto);
		Task<Player[]> Get();
	}
}
