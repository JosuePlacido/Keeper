using System.Collections.Generic;
using System.Linq;
using System;
using Keeper.Domain.Core;
using Keeper.Domain.Enum;

namespace Keeper.Domain.Models
{
	public class Group : Entity
	{
		public string Name { get; private set; }
		public string StageId { get; private set; }
		public IList<Match> Matchs { get; private set; }
		public IList<Vacancy> Vacancys { get; private set; }
		public IList<Statistic> Statistics { get; private set; }
		private Group() { }
		public Group(string name) : base(Guid.NewGuid().ToString())
		{
			Name = name;
			Statistics = new List<Statistic>();
			Vacancys = new List<Vacancy>();
			Matchs = new List<Match>();
		}
		public Group AddMatches(Match[] matches)
		{
			foreach (var match in matches)
			{
				Matchs.Add(match);
			}
			return this;
		}
		public IList<Match> RoundRobinMatches(bool duplicateTurn = false, bool mirrorTurn = false)
		{
			Matchs = new List<Match>();
			IDictionary<string, bool> teams = new Dictionary<string, bool>();
			if (Statistics != null)
			{
				foreach (var team in Statistics)
				{
					teams.Add(team.TeamSubscribeId, false);
				}
			}
			if (Vacancys != null)
			{
				foreach (var vacancy in Vacancys)
				{
					teams.Add(vacancy.Id, true);
				}
			}
			int totalTeams = teams.Count;
			int totalRound = totalTeams - 1 + totalTeams % 2;
			int gamesPerRound = totalTeams / 2;
			int roundReturn = 0;
			if (duplicateTurn)
				roundReturn = mirrorTurn ? -totalRound * 2 - 1 : totalRound;
			List<string> teamsA = new List<string>(teams.Keys.Take(gamesPerRound));
			List<string> teamsB = new List<string>();
			if (teams.Count % 2 != 0)
			{
				teamsA.Insert(0, null);
			}
			teamsB.AddRange(teams.Keys.Skip(gamesPerRound).Reverse().ToArray());
			for (int round = 1; round <= totalRound; round++)
			{
				int matchtsCreated = 0;
				for (int game = 0; matchtsCreated < gamesPerRound; game++)
				{
					if (teamsA[0] != null || game != 0)
					{
						var homeVacancy = false;
						var awayVacancy = false;
						teams.TryGetValue(teamsA[game], out homeVacancy);
						teams.TryGetValue(teamsB[game], out awayVacancy);
						if (game % 2 == 1 || (game == 0 && round % 2 == 1))
						{
							AddMatch(round, game, teamsA[game], teamsB[game], homeVacancy, awayVacancy);
							if (roundReturn != 0)
							{
								AddMatch(Math.Abs(round + roundReturn), game, teamsB[game], teamsA[game], awayVacancy, homeVacancy);
							}
						}
						else
						{
							AddMatch(round, game, teamsB[game], teamsA[game], awayVacancy, homeVacancy);
							if (roundReturn != 0)
							{
								AddMatch(Math.Abs(round + roundReturn), game, teamsA[game], teamsB[game], homeVacancy, awayVacancy);
							}
						}
						matchtsCreated++;
					}
				}
				teamsA.Insert(1, teamsB[0]);
				teamsB.RemoveAt(0);
				teamsB.Add(teamsA.Last());
				teamsA.RemoveAt(teamsA.Count - 1);
			}
			return Matchs;
		}

		public void AddTeam(string teamId)
		{
			Statistics.Add(new Statistic(teamId));
		}

		private void AddMatch(int round, int game, string home, string away, bool homeVacancy, bool awayVacancy)
		{
			if (homeVacancy == awayVacancy)
			{
				if (homeVacancy)
				{
					Matchs.Add(new Match(round, Status.Created, $"Rodada {round} - Jogo {game + 1}", vacancyHome: home, vacancyAway: away));
				}
				else
				{
					Matchs.Add(new Match(round, Status.Created, $"Rodada {round} - Jogo {game + 1}", home, away));
				}
			}
			else
			{
				if (homeVacancy)
				{
					Matchs.Add(new Match(round, Status.Created, $"Rodada {round} - Jogo {game + 1}", vacancyHome: home, away: away));
				}
				else
				{
					Matchs.Add(new Match(round, Status.Created, $"Rodada {round} - Jogo {game + 1}", home, vacancyAway: away));
				}
			}
		}
		public override string ToString()
		{
			return Name;
		}

		public static Group Factory(string id, string name,
			string stageId, IList<Match> matchs = null,
			IList<Vacancy> vacancys = null, IList<Statistic> statistics = null)
		{
			return new Group
			{
				Id = id,
				StageId = stageId,
				Matchs = matchs,
				Vacancys = vacancys,
				Statistics = statistics,
				Name = name
			};
		}
	}
}
