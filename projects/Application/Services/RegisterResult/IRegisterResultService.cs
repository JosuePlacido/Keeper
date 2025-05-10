using System;
using System.Threading.Tasks;
using Application.Contract;
using Domain.Models;

namespace Application.Services.RegisterResult
{
	public interface IRegisterResultService : IDisposable
	{
		Task<IServiceResponse> RegisterResult(MatchResultDTO dto);
		Task<Match> GetMatch(string id);
	}
}
