using System;
using System.Threading.Tasks;
using Application.Contract;

namespace Application.Services.CreateChampionship
{
	public interface IChampionshipService : IDisposable
	{
		Task<IServiceResponse> Create(ChampionshipCreateDTO dto);
	}
}
