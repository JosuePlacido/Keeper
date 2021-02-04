using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Application.DTO
{
	public class RankDTO : IDTO
	{
		public string Name { get; set; }
		public StageRankDTO[] Stages { get; set; }
	}
	public class StageRankDTO
	{
		public string Name { get; set; }
		public GroupRankDTO[] Groups { get; set; }
	}
	public class GroupRankDTO
	{
		public string Name { get; set; }
		public RankModel[] Ranks { get; set; }
	}
	public class RankModel
	{
		public string Id { get; set; }
		public string Team { get; set; }
		public int Games { get; set; }
		public int Won { get; set; }
		public int Drowns { get; set; }
		public int Lost { get; set; }
		public int GoalsScores { get; set; }
		public int Position { get; set; }
		public int GoalsAgainst { get; set; }
		public int GoalsDifference { get; set; }
		public int Yellows { get; set; }
		public int Reds { get; set; }
		public int Points { get; set; }
		public string Lastfive { get; set; }
		public int RankMovement { get; set; }
	}
	public class RankPost
	{
		[Required(ErrorMessage = "Id é um campo obrigatório")]
		public string Id { get; set; }
		public int? Games { get; set; }
		public int? Won { get; set; }
		public int? Drowns { get; set; }
		public int? Lost { get; set; }
		public int? GoalsScores { get; set; }
		public int? Position { get; set; }
		public int? GoalsAgainst { get; set; }
		public int? GoalsDifference { get; set; }
		public int? Yellows { get; set; }
		public int? Reds { get; set; }
		public int? Points { get; set; }
		public string Lastfive { get; set; }
		public int? RankMovement { get; set; }
	}
}
