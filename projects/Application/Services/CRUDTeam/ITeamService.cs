using System;
using System.Threading.Tasks;
using Domain.Models;
using Application.Contract;

namespace Application.Services.CRUDTeam
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
