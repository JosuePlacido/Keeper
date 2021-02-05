using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Api.Controllers
{
	//[Authorize]
	[Route("[controller]")]
	public class ChampionshipController : ApiController
	{
		private readonly IChampionshipService _ChampionshipAppService;

		public ChampionshipController(IChampionshipService ChampionshipAppService)
		{
			_ChampionshipAppService = ChampionshipAppService;
		}
		[HttpGet("Squads/{id}")]
		public async Task<SquadEditDTO[]> Squads(string id)
		{
			return await _ChampionshipAppService.GetSquads(id);
		}
		[HttpPost("Squad")]
		public async Task<IActionResult> Squad(PLayerSquadPostDTO[] squads)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.UpdateSquad(squads));
		}
		[HttpPost]
		public async Task<IActionResult> Post(ChampionshipCreateDTO dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.Create(dto));
		}
		[HttpGet("Names/{id}")]
		public async Task<ObjectRenameDTO> Names(string id)
		{
			return await _ChampionshipAppService.GetNames(id);
		}
		[HttpPost("Names")]
		public async Task<IActionResult> Names(ObjectRenameDTO dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.RenameScopes(dto));
		}
		[HttpGet("Ranks/{id}")]
		public async Task<RankDTO> Ranks(string id)
		{
			return await _ChampionshipAppService.Rank(id);
		}
		[HttpPost("Ranks")]
		public async Task<IActionResult> Ranks(RankPost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.UpdateStatistics(dto));
		}
		[HttpGet("Teams/{id}")]
		public async Task<TeamStatisticDTO[]> Teams(string id)
		{
			return await _ChampionshipAppService.TeamStats(id);
		}
		[HttpPost("Teams")]
		public async Task<IActionResult> Teams(TeamSubscribePost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.UpdateTeamsStatistics(dto));
		}
		[HttpGet("Players/{id}")]
		public async Task<PlayerStatisticDTO[]> Player(string id)
		{
			return await _ChampionshipAppService.PlayerStats(id);
		}
		[HttpPost("Players")]
		public async Task<IActionResult> Players(PlayerSubscribePost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.UpdatePlayersStatistics(dto));
		}
		[HttpGet("Matches/{id}")]
		public async Task<MatchEditsScope> Matches(string id)
		{
			return await _ChampionshipAppService.GetMatchSchedule(id);
		}
		[HttpPost("Matches")]
		public async Task<IActionResult> Matches(MatchEditsScope dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _ChampionshipAppService.UpdateMatches(dto));
		}
		[HttpPost("Match/Check")]
		public async Task<IActionResult> CheackMatches(MatchEditsScope dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(_ChampionshipAppService.CheckMatches(dto));
		}
	}
}
