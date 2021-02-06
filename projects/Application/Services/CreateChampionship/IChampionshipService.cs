using System;
using System.Threading.Tasks;
using Keeper.Application.Contract;

namespace Keeper.Application.Services.CreateChampionship
{
	public interface IChampionshipService : IDisposable
	{
		Task<IServiceResponse> Create(ChampionshipCreateDTO dto);
	}
}
