using System.Collections.Generic;
using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Application.Contract;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Api.Controllers
{
	//[Authorize]
	[Route("[controller]")]
	public class TeamController : ApiController
	{
		private readonly ITeamService _TeamAppService;

		public TeamController(ITeamService TeamAppService)
		{
			_TeamAppService = TeamAppService;
		}

		public async Task<IEnumerable<Team>> Get()
		{
			return await _TeamAppService.List();
		}

		[HttpGet("{id}")]
		public async Task<Team> Get(string id)
		{
			return await _TeamAppService.Get(id);
		}
		[HttpPost]
		public async Task<IActionResult> Post(TeamCreateDTO TeamViewModel)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _TeamAppService.Create(TeamViewModel));
		}
		[HttpPut]
		public async Task<IActionResult> Put(TeamUpdateDTO TeamViewModel)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _TeamAppService.Update(TeamViewModel));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _TeamAppService.Delete(id));
		}
		[HttpGet("Availables")]
		public async Task<TeamPaginationDTO> Availables(string terms = null,
			string notInChampinship = null, int page = 1, int take = 30)
		{
			return await _TeamAppService
				.GetTeamsAvailablesForChampionship(terms, notInChampinship, page, take);
		}
	}
}
