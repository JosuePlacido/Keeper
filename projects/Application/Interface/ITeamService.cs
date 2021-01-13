using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.CrossCutting.DTO;

namespace Application.Interface
{
	interface ITeamService
	{

		Task<Team> Create(TeamCreateDTO dto);

		Task<Team> Delete(Team dto);

		Task<Team> Get(string id);
		Task<Team[]> List();

		Task<Team> Update(TeamUpdateDTO dto);
	}
}
