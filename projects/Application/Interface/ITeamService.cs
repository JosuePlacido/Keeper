using System;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Application.Interface
{
	public interface ITeamService : IDisposable
	{

		Task<Team> Create(TeamCreateDTO dto);

		Task<IServiceResult> Delete(string dto);

		Task<Team> Get(string id);
		Task<Team[]> List();

		Task<IServiceResult> Update(TeamUpdateDTO dto);
	}
}
