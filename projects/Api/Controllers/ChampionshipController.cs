using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;
using Keeper.Infrastructure.DAO;
using Microsoft.AspNetCore.Authorization;
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
	}
}
