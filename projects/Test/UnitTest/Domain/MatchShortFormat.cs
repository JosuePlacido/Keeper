using Domain.Models;

namespace Test.Domain
{
	public class MatchShortFormat
	{
		public int Round { get; }
		public string Home { get; }
		public string Away { get; }

		public MatchShortFormat(Match match)
		{
			Round = match.Round;
			Home = string.IsNullOrEmpty(match.HomeId) ? match.VacancyHomeId : match.HomeId;
			Away = string.IsNullOrEmpty(match.AwayId) ? match.VacancyAwayId : match.AwayId;
		}

		public override string ToString()
		{
			return $"{Home} vs {Away}";
		}
	}
}
