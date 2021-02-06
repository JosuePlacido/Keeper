using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Models;

namespace Keeper.Application.Services.EditChampionship
{
	public class SquadEditDTO
	{
		public string ChampionshipId { get; set; }
		public string TeamId { get; set; }
		public Team Team { get; set; }
		public PLayerSquadEditDTO[] Players { get; set; }
	}
	public class PLayerSquadEditDTO
	{
		public string TeamSubscribeId { get; set; }
		public string Id { get; set; }
		public string PlayerId { get; set; }
		public string PlayerName { get; set; }
		public Player PlayerNick { get; set; }
	}
	public class PLayerSquadPostDTO
	{
		public string Id { get; set; }
		public string TeamSubscribeId { get; set; }
		public string PlayerId { get; set; }
		public string PlayerName { get; set; }
		public string Status { get; set; }

		public PLayerSquadPostDTO(string id, string playerId, string teamSubscribeId, string playerName, string status)
		{
			Id = id;
			TeamSubscribeId = teamSubscribeId;
			PlayerId = playerId;
			PlayerName = playerName;
			Status = status;
		}
	}
}
