using System.Threading.Tasks;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Application.Services.MatchService;
using Application.Services.RegisterResult;

namespace Api.Controllers;
//[Authorize]
[Route("[controller]")]
public class MatchController : ApiController
{
	private readonly IMatchService _service;
	private readonly IRegisterResultService _registerResultService;

	public MatchController(IMatchService MatchAppService, IRegisterResultService result)
	{
		_service = MatchAppService;
		_registerResultService = result;
	}

	[HttpGet("Schedule/{id}")]
	public async Task<MatchEditsScope> Matches(string id)
	{
		return await _service.GetMatchSchedule(id);
	}
	[HttpPost("Schedule")]
	public async Task<IActionResult> Matches(MatchEditedDTO[] dto) =>
			await CallApplicationAsync(_service.UpdateMatches(dto));

	[HttpPost("Check")]
	public IActionResult CheackMatches(MatchEditsScope dto) =>
			Ok(_service.CheckMatches(dto));

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(string id)
	{
		return await CallApplicationAsync(_registerResultService.GetMatch(id));
	}
	[HttpPost]
	public async Task<IActionResult> Post(MatchResultDTO dto) =>
			await CallApplicationAsync(_registerResultService.RegisterResult(dto));

}
