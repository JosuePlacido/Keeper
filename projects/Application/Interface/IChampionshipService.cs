using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Keeper.Application.Models;
using Keeper.Application.DTO;
using Keeper.Domain.Models;

namespace Keeper.Application.Interface
{
	public interface IChampionshipService : IDisposable
	{
		Task<IServiceResult> Create(ChampionshipCreateDTO dto);
		MatchEditsScope CheckMatches(MatchEditsScope dto);
		Task<SquadEditDTO[]> GetSquads(string championship);
		Task<IServiceResult> UpdateSquad(PLayerSquadPostDTO[] squads);
	}
}
