using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Enum;

namespace Keeper.Application.Services.RegisterResult
{
	public class MatchResultDTO
	{
		[Required(ErrorMessage = "Campo obrigatório")]
		public string Id { get; set; }
		[Required(ErrorMessage = "Campo obrigatório")]
		public int GoalsHome { get; set; }
		[Required(ErrorMessage = "Campo obrigatório")]
		public int GoalsAway { get; set; }
		public int? GoalsPenaltyHome { get; set; }
		public int? GoalsPenaltyAway { get; set; }
		public EventGameDTO[] Events { get; set; }
	}
	public class EventGameDTO
	{
		public string Description { get; set; }
		public TypeEvent Type { get; set; }
		public bool IsHomeEvent { get; set; }
		public string MatchId { get; set; }
		public string PlayerId { get; set; }
	}
}
