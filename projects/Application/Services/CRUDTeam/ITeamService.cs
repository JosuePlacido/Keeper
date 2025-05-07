using System;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTO;

namespace Application.Services.CRUDTeam;
public interface ITeamService : IDisposable
{
	Task<Team> Create(TeamCreateDTO dto);
	Task<Team> Delete(string dto);
	Task<Team> Get(string id);
	Task<Team> Update(TeamUpdateDTO dto);
	Task<PaginationDTO<Team>> List(string terms, int page, int take);
	Task<PaginationDTO<Team>> GetTeamsAvailablesForChampionship(string terms,
		string notInChampinship, int page, int take);
}