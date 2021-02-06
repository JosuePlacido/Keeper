using System;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;

namespace Keeper.Application.Contract
{
	public interface ITeamService : IDisposable
	{

		Task<Team> Create(TeamCreateDTO dto);

		Task<IServiceResponse> Delete(string dto);

		Task<Team> Get(string id);
		Task<Team[]> List();

		Task<IServiceResponse> Update(TeamUpdateDTO dto);
		Task<TeamPaginationDTO> GetTeamsAvailablesForChampionship(string terms,
			string notInChampinship, int page, int take);
	}
}
