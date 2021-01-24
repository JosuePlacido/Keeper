using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Keeper.Application.Models;
using Keeper.Application.DTO;

namespace Keeper.Application.Interface
{
	public interface IChampionshipService : IDisposable
	{
		Task<CreateChampionshipResponse> Create(ChampionshipCreateDTO dto);
		MatchEditsScope CheckMatches(MatchEditsScope dto);
		Task<SquadEditDTO[]> GetSquads(string championship);
	}
}
