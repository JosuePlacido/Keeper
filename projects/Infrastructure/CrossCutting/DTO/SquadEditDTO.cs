using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Models;

namespace Keeper.Infrastructure.CrossCutting.DTO
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
		public string TeamSubscribeId { get; private set; }
		public string Id { get; private set; }
		public string PlayerId { get; private set; }
		public string PlayerName { get; private set; }
		public Player PlayerNick { get; private set; }
	}
}
