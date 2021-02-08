using System;
using System.Collections.Generic;

namespace Keeper.Application.Services.MatchService
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
	public class MatchEditedDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public DateTime? Date { get; set; }
		public string VacancyHomeId { get; set; }
		public string VacancyAwayId { get; set; }
		public string HomeId { get; set; }
		public string AwayId { get; set; }
		public int? Round { get; set; }
		public bool? FinalGame { get; set; }
		public bool? AggregateGame { get; set; }
		public bool? Penalty { get; set; }
		public string Status { get; set; }
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
