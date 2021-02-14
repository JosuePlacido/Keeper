using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.Services.EditChampionship
{
	public class TeamStatisticDTO
	{
		public string Id { get; set; }
		public string Team { get; set; }
		public int Games { get; set; }
		public int Won { get; set; }
		public int Drowns { get; set; }
		public int Lost { get; set; }
		public int GoalsScores { get; set; }
		public int GoalsAgainst { get; set; }
		public int GoalsDifference { get; set; }
		public int Yellows { get; set; }
		public int Reds { get; set; }
	}
	public class PlayerStatisticDTO
	{
		public string Id { get; set; }
		public string Player { get; set; }
		public int Games { get; set; }
		public int Goals { get; set; }
		public int YellowCard { get; set; }
		public int RedCard { get; set; }
		public int MVPs { get; set; }
	}
	public class TeamSubscribePost
	{
		public string Id { get; set; }
		public int? Games { get; set; }
		public int? Won { get; set; }
		public int? Drowns { get; set; }
		public int? Lost { get; set; }
		public int? GoalsScores { get; set; }
		public int? GoalsAgainst { get; set; }
		public int? GoalsDifference { get; set; }
		public int? Yellows { get; set; }
		public int? Reds { get; set; }
	}
	public class PlayerSubscribePost
	{
		public string Id { get; set; }
		public int? Games { get; set; }
		public int? Goals { get; set; }
		public int MVPs { get; private set; }
		public int? Yellows { get; set; }
		public int? Reds { get; set; }
	}
}