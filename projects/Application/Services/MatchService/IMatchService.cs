using System;
using System.Threading.Tasks;
using Application.Contract;
using Application.DTO;

namespace Application.Services.MatchService
{
	public interface IMatchService : IDisposable
	{
		MatchEditsScope CheckMatches(MatchEditsScope dto);
		Task<MatchEditsScope> GetMatchSchedule(string id);
		Task<IServiceResponse> UpdateMatches(MatchEditedDTO[] dto);
	}
}
