using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Application.Contract;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Keeper.Application.Services.MatchService;

namespace Keeper.Api.Controllers
{
	//[Authorize]
	[Route("[controller]")]
	public class MatchController : ApiController
	{
		private readonly IMatchService _service;

		public MatchController(IMatchService MatchAppService)
		{
			_service = MatchAppService;
		}

		[HttpGet("Schedule/{id}")]
		public async Task<MatchEditsScope> Matches(string id)
		{
			return await _service.GetMatchSchedule(id);
		}
		[HttpPost("Schedule")]
		public async Task<IActionResult> Matches(MatchEditedDTO[] dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(await _service.UpdateMatches(dto));
		}
		[HttpPost("Check")]
		public async Task<IActionResult> CheackMatches(MatchEditsScope dto)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) :
				CustomResponse(_service.CheckMatches(dto));
		}
	}
}
