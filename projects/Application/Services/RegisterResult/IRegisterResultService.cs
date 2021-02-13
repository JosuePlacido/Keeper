using System;
using System.Threading.Tasks;
using Keeper.Application.Contract;
using Keeper.Domain.Models;

namespace Keeper.Application.Services.RegisterResult
{
	public interface IRegisterResultService : IDisposable
	{
		Task<IServiceResponse> RegisterResult(MatchResultDTO dto);
		Task<Match> GetMatch(string id);
	}
}
