using System.Threading.Tasks;
using Keeper.Application.Services.CreateChampionship;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Api.Controllers
{
	//[Authorize]
	[Route("[controller]")]
	public class ChampionshipController : ApiController
	{
		private readonly IChampionshipService _createService;
		private readonly IEditChampionshipService _editService;

		public ChampionshipController(IChampionshipService ChampionshipAppService,
			IEditChampionshipService editService)
		{
			_createService = ChampionshipAppService;
			_editService = editService;
		}
		[HttpGet("Squads/{id}")]
		public async Task<SquadEditDTO[]> Squads(string id)
		{
			return await _editService.GetSquads(id);
		}
		[HttpPost("Squad")]
		public async Task<IActionResult> Squad(PLayerSquadPostDTO[] squads)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _editService.UpdateSquad(squads));
		}
		[HttpPost]
		public async Task<IActionResult> Post(ChampionshipCreateDTO dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _createService.Create(dto));
		}
		[HttpGet("Names/{id}")]
		public async Task<ObjectRenameDTO> Names(string id)
		{
			return await _editService.GetNames(id);
		}
		[HttpPost("Names")]
		public async Task<IActionResult> Names(ObjectRenameDTO dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _editService.RenameScopes(dto));
		}
		[HttpGet("Ranks/{id}")]
		public async Task<RankDTO> Ranks(string id)
		{
			return await _editService.Rank(id);
		}
		[HttpPost("Ranks")]
		public async Task<IActionResult> Ranks(RankPost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _editService.UpdateStatistics(dto));
		}
		[HttpGet("Teams/{id}")]
		public async Task<TeamStatisticDTO[]> Teams(string id)
		{
			return await _editService.TeamStats(id);
		}
		[HttpPost("Teams")]
		public async Task<IActionResult> Teams(TeamSubscribePost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _editService.UpdateTeamsStatistics(dto));
		}
		[HttpGet("Players/{id}")]
		public async Task<PlayerStatisticDTO[]> Player(string id)
		{
			return await _editService.PlayerStats(id);
		}
		[HttpPost("Players")]
		public async Task<IActionResult> Players(PlayerSubscribePost[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _editService.UpdatePlayersStatistics(dto));
		}
	}
}
