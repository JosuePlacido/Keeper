using System;
using System.Threading.Tasks;
using Keeper.Application.Contract;
using Keeper.Application.DTO;

namespace Keeper.Application.Services.MatchService
{
	public interface IMatchService : IDisposable
	{
		MatchEditsScope CheckMatches(MatchEditsScope dto);
		Task<MatchEditsScope> GetMatchSchedule(string id);
		Task<IServiceResponse> UpdateMatches(MatchEditedDTO[] dto);
	}
}
