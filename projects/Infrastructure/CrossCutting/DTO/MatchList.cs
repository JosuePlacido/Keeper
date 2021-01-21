using System;
using System.Collections.Generic;

namespace Keeper.Infrastructure.CrossCutting.DTO
{
	public static class MatchAuditoryContants
	{
		public static string SEQUENCE_SAME_FIELD = "O time {0} está com muitos de jogos ({1}) como {2}, nas Rodadas {3}.";
		public static string HOME_AWAY_GAP = "O time {0} está com quantidade incorreta de jogos como Mandante ({1}) e Visitante({2})";
		public static string UNIQUE_MATCH = "O jogo {0} está duplicado!";
		public static string MATCH_COUNT = "O time {0} está com quantidade incorreta de jogos ({1})!";

		public static string GenerateMessage(string msg, params object[] args)
		{
			return String.Format(msg, args).Replace("#h", "Mandante").Replace("#a", "Visitante");
		}

	}
	public class MatchEditsScope
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public IList<string> Errors { get; set; }
		public MatchStageEdit[] Stages { get; set; }
	}
	public class MatchStageEdit
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public bool DuplicateTurn { get; set; }
		public MatchGroupEdit[] Groups { get; set; }
	}
	public class MatchGroupEdit
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string StageId { get; set; }
		public MatchItemDTO[] Matchs { get; set; }
	}

	public class MatchEditVacancy
	{
		public string Description { get; set; }
	}
	public class MatchEditTeam
	{
		public string Team { get; set; }
	}
	public class MatchItemDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime? Date { get; set; }
		public MatchEditVacancy VacancyHome { get; set; }
		public string VacancyHomeId { get; set; }
		public string VacancyAwayId { get; set; }
		public MatchEditVacancy VacancyAway { get; set; }
		public MatchEditTeam Home { get; set; }
		public string HomeId { get; set; }
		public MatchEditTeam Away { get; set; }
		public string AwayId { get; set; }
		public int Round { get; set; }
		public string Status { get; set; }
		public bool HasError { get; set; }
		public override string ToString()
		{

			return string.Format("{0} x {1}",
				(string.IsNullOrEmpty(HomeId)) ? VacancyHome.Description : Home.Team,
				(string.IsNullOrEmpty(AwayId)) ? VacancyAway.Description : Away.Team);
		}
		public override bool Equals(object obj)
		{
			return obj is MatchItemDTO @entity &&
				HomeId == @entity.HomeId &&
				AwayId == @entity.AwayId &&
				VacancyHomeId == @entity.VacancyHomeId &&
				VacancyAwayId == @entity.VacancyAwayId;
		}

		public override int GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.Add(Id);
			return hash.ToHashCode();
		}
	}

	public class AuditoryMatch
	{
		public AuditoryMatch()
		{
			MatchesIdSequence = new List<string>();
			MatchesRoundSequence = new List<int>();
		}

		public int TotalMatches { get; set; }
		public int TotalHomeMatches { get; set; }
		public int TotalAwayMatches { get; set; }
		public int HomAwayGap { get; set; }
		public string LastField { get; set; }
		public int SameFieldSequencie { get; set; }
		public List<string> MatchesIdSequence { get; set; }
		public List<int> MatchesRoundSequence { get; set; }
	}
}
