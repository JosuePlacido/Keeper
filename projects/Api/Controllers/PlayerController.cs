using System.Threading.Tasks;
using Keeper.Application.DTO;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Api.Controllers
{
	//[Authorize]
	[Route("[controller]")]
	public class PlayerController : ApiController
	{
		private readonly IPlayerService _PlayerAppService;

		public PlayerController(IPlayerService PlayerAppService)
		{
			_PlayerAppService = PlayerAppService;
		}

		public async Task<PlayerPaginationDTO> Get(string terms = null,
			string notInChampinship = null, int page = 1, int take = 10)
		{
			return await _PlayerAppService.GetAvailables(terms, notInChampinship, page, take);
		}

		[HttpGet("{id}")]
		public async Task<Player> Get(string id)
		{
			return await _PlayerAppService.Get(id);
		}
		[HttpPost]
		public async Task<IActionResult> Post(PlayerCreateDTO PlayerViewModel)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _PlayerAppService.Create(PlayerViewModel));
		}
		[HttpPut]
		public async Task<IActionResult> Put(PlayerUpdateDTO PlayerViewModel)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _PlayerAppService.Update(PlayerViewModel));
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _PlayerAppService.Delete(id));
		}
	}
}
