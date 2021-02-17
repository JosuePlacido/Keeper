using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Domain.Models;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Domain.Enum
{
	public class TiebreackCriterion : Enumeration
	{
		public static TiebreackCriterion Points = new TiebreackCriterion(0,
			"Pontos", (r => r.Points), true);
		public static TiebreackCriterion Won = new TiebreackCriterion(1,
			"Vitórias", (r => r.Won), true);
		public static TiebreackCriterion GoalsBalance = new TiebreackCriterion(2,
			"Saldo de gols", (r => r.GoalsDifference), true);
		public static TiebreackCriterion Goals = new TiebreackCriterion(3,
			"Gols marcados", (r => r.GoalsScores), true);
		public static TiebreackCriterion RedCard = new TiebreackCriterion(4,
			"Cartões Vermelhos", (r => r.Reds));
		public static TiebreackCriterion YellowCard = new TiebreackCriterion(5,
			"Cartões amarelos", (r => r.Yellows));
		public static TiebreackCriterion GoalsAgainst = new TiebreackCriterion(6,
			"Gols sofridos", (r => r.GoalsAgainst));
		public static TiebreackCriterion DirectMatch = new TiebreackCriterion(7,
			"Confronto direto", (r => 0), true, true,
			(teams, loadMatches) =>
			{
				Statistic[] teamsDroweds = teams.Where(t => teams.Where(s => s.Position == t.Position).Count() > 1).ToArray();
				if (teamsDroweds.Length != 0)
				{
					int[] RanksDrowed = teams.Select(t => t.Position).Distinct().ToArray();
					SortTeamModelHelper[] teamsDirectRank;
					Match[] matches;
					foreach (var rank in RanksDrowed)
					{
						teamsDirectRank = teamsDroweds.Where(td => td.Position == rank)
							.Select(td => new SortTeamModelHelper(td)).ToArray();

						matches = loadMatches(teams[0].GroupId, teamsDirectRank.
							Select(tdr => tdr.TeamSubscribeId).ToArray());
						if (matches.Length > 0)
						{
							foreach (var match in matches)
							{
								teamsDirectRank.Where(td => td.TeamSubscribeId == match.HomeId)
									.FirstOrDefault().RegisterResult(
										(int)match.GoalsHome, (int)match.GoalsAway);

								teamsDirectRank.Where(td => td.TeamSubscribeId == match.AwayId)
									.FirstOrDefault().RegisterResult(
										(int)match.GoalsAway, (int)match.GoalsHome);
							};

							teamsDirectRank = teamsDirectRank
								.OrderByDescending(v => v.Points)
									.ThenByDescending(v => v.Won)
										.ThenByDescending(v => v.GoalsScores)
											.ThenByDescending(v => v.GoalsDifference)
												.ThenBy(v => v.Games)
								.ToArray();

							int currentPosition = teamsDirectRank[0].Position;
							int teamsDrowed = 1;
							teamsDirectRank = teamsDirectRank.Select((tdr, index) =>
							{
								if (index > 0)
								{
									if (tdr.Points == teamsDirectRank[index - 1].Points
										&& tdr.Won == teamsDirectRank[index - 1].Won
										&& tdr.GoalsScores == teamsDirectRank[index - 1].GoalsScores
										&& tdr.GoalsDifference == teamsDirectRank[index - 1].GoalsDifference
										&& tdr.Games == teamsDirectRank[index - 1].Games)
									{
										teamsDrowed++;
									}
									else
									{
										currentPosition += teamsDrowed;
										teamsDrowed = 1;
									}
									tdr.Position = currentPosition;
								}
								return tdr;
							}).ToArray();


							foreach (var newRank in teamsDirectRank)
							{
								teams.Where(t => t.Position == rank &&
									t.TeamSubscribeId == newRank.TeamSubscribeId).
									FirstOrDefault().Reorder(newRank.Position);
							}

						}
					}
				}
				return teams;
			});
		public static TiebreackCriterion GolsAsAway = new TiebreackCriterion(8,
			"Gols como visitante", (r => 0), true, true,
			(teams, loadMatches) =>
			{
				Statistic[] teamsDrowed = teams.Where(t => teams.Where(s => s.Position == t.Position).Count() > 1).ToArray();
				if (teamsDrowed.Length != 0)
				{
					IDictionary<string, int> teamsDirectRank =
						new Dictionary<string, int>();
					foreach (var team in teamsDrowed)
					{
						teamsDirectRank.Add(team.TeamSubscribeId, team.Position);
					}
					Match[] matches = loadMatches(teams[0].GroupId, teamsDirectRank.Select(tdr => tdr.Key).ToArray());
					foreach (var match in matches)
					{
						teamsDirectRank[match.AwayId] += (int)match.GoalsAway;
					};
					string[] orderedTeams = teamsDirectRank
						.OrderByDescending(tdr => tdr.Value).Select(tdr => tdr.Key).ToArray();

					int position = teamsDirectRank[orderedTeams[0]];
					int teamsDrowedCount = 1;
					for (int x = 1; x < orderedTeams.Length; x++)
					{
						if (teamsDirectRank[orderedTeams[x]] == teamsDirectRank[orderedTeams[x - 1]])
						{
							teamsDrowedCount++;
						}
						else
						{
							position += teamsDrowedCount;
							teamsDrowedCount = 1;
						}
						teams.Where(t => t.TeamSubscribeId == orderedTeams[x])
							.FirstOrDefault().Reorder(position);
					}
				}
				return teams;
			});
		public static TiebreackCriterion ExtraMatch = new TiebreackCriterion(9,
			"Jogo desempate", (r => 0), true, true, ((teams, loadMatches) => teams));
		public static TiebreackCriterion Penalties = new TiebreackCriterion(10,
			"Penaltis", (r => 0), true, true, ((teams, loadMatches) =>
			{
				if (teams[0].Position == teams[1].Position)
				{
					Match[] matches = loadMatches(teams[0].GroupId, teams.Select(t => t.TeamSubscribeId).ToArray());
					Match matchWithPenalties = matches.Where(m => m.FinalGame && m.Penalty)
						.FirstOrDefault();
					int result = (int)matchWithPenalties.GoalsPenaltyHome - (int)matchWithPenalties
						.GoalsPenaltyAway;
					Statistic team;
					if (result > 0)
					{
						team = teams.Where(t => t.TeamSubscribeId == matchWithPenalties.AwayId)
							.FirstOrDefault();
					}
					else
					{
						team = teams.Where(t => t.TeamSubscribeId == matchWithPenalties.HomeId)
							.FirstOrDefault();
					}
					team.Reorder(team.Position + 1);
				}
				return teams.OrderBy(t => t.Position).ToArray();
			}));
		public static TiebreackCriterion Random = new TiebreackCriterion(11,
			"Sorteio", (r => 0), true, true, ((teams, loadMatches) =>
			{
				int randomResult;
				for (int x = 1; x < teams.Length; x++)
				{
					if (teams[x].Position <= teams[x - 1].Position)
					{
						randomResult = new Random().Next(0, 2);
						teams[x - randomResult]
							.Reorder(teams[x - randomResult].Position + 1);
						if (randomResult == 1)
						{
							var aux = teams[x];
							teams[x] = teams[x - 1];
							teams[x - 1] = aux;
						}
					}
				}
				return teams;
			}));
		private readonly Func<Statistic, int> Criterion;
		public Func<Statistic[], Func<string, string[], Match[]>, Statistic[]> ComplexCriterion;
		private readonly bool IsDescending;
		private readonly bool IsComplex;
		private TiebreackCriterion(int id, string name, Func<Statistic, int> criterion,
			bool isDescending = false, bool isComplex = false,
			Func<Statistic[], Func<string, string[], Match[]>, Statistic[]> complex = null) : base(id, name)
		{
			Criterion = criterion;
			IsDescending = isDescending;
			IsComplex = isComplex;
			ComplexCriterion = complex;
		}
		public static IEnumerable<TiebreackCriterion> GetAll()
		{
			return TiebreackCriterion.GetAll<TiebreackCriterion>();
		}
		public Statistic[] Order(Statistic[] teams, Func<string, string[], Match[]> callback = null)
		{
			if (IsComplex)
			{
				teams = ComplexCriterion.Invoke(teams, callback);
			}
			else
			{
				int position = 1;
				int teamsDrowed = 1;
				bool Drowed = true;
				if (Id == Points.Id)
				{
					teams = teams.OrderByDescending(Criterion).ToArray();
					if (teams[0].Position != 1)
					{
						teams[0].Reorder(1);
					}
				}
				else
				{
					teams = IsDescending ? teams.OrderBy(t => t.Position)
						.ThenByDescending(Criterion).ToArray()
						: teams.OrderBy(t => t.Position).ThenBy(Criterion).ToArray();
				}
				for (int x = 1; x < teams.Length; x++)
				{
					Drowed = Criterion.Invoke(teams[x]) == Criterion.Invoke(teams[x - 1])
						&& (Id == Points.Id || teams[x].Position == teams[x - 1].Position);
					if (Drowed)
					{
						teamsDrowed++;
					}
					else
					{
						position += teamsDrowed;
						teamsDrowed = 1;
					}
					teams[x].Reorder(position);
				}
			}
			var test = teams.Select(t => t.Position).ToArray();
			return teams;
		}
	}
}
