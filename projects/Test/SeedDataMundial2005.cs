using System;
using System.Linq;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;

namespace Keeper.Test
{
	public static class SeedData2
	{
		public static Championship Championship = Championship.Factory("c2",
				"Mundial de Cluebs FIFA", "2005", SeedData.Categorys[2],
				Status.Finish);
		public static Team[] Teams = new Team[] {
							Team.Factory("liverpool","Liverpool"),
							Team.Factory("allitihad","All Itihad"),
							Team.Factory("saprissa","Saprissa"),
		};
		public static TeamSubscribe[] TeamsSubscribes = new TeamSubscribe[]
						{
					TeamSubscribe.Factory("c2spfc","t5",
							championshipId:Championship.Id),
					TeamSubscribe.Factory("c2liverpool",Teams[0].Id,
							championshipId:Championship.Id),
					TeamSubscribe.Factory("c2allitihad",Teams[1].Id,
							championshipId:Championship.Id),
					TeamSubscribe.Factory("c2saprissa",Teams[2].Id,
							championshipId:Championship.Id)
						};
		public static Stage[] Stages = new Stage[]
				{
					Stage.Factory(
						"c2semi",
						Championship.Id,
						criterias: "0,1,2,8,10",
						name: "Semi-Final",
						order: 0,
						typeStage: TypeStage.Knockout,
						regulation: Classifieds.Configured
					),
					Stage.Factory(
						"c2final",
						Championship.Id,
						criterias: "0,1,2,8,10",
						name: "Final",
						order: 1,
						typeStage: TypeStage.Knockout,
						regulation: Classifieds.Configured
					)
				};

		public static Group[] Groups = new Group[]
		{
			Group.Factory("c2semia","Semi-Final 1", Stages[0].Id),
			Group.Factory("c2semib","Semi-Final 2", Stages[0].Id),
			Group.Factory("c2final","Final", Stages[1].Id)
		};

		public static Vacancy[] Vacancys = new Vacancy[] {
				Vacancy.Factory("c2finala","Vencedor Semi-final 1",Groups[2].Id, Classifieds.Configured,
					Groups[0].Id,0,1),
				Vacancy.Factory("c2finalb","Vencedor Semi-final 2",Groups[2].Id, Classifieds.Configured,
					Groups[1].Id,0,1),
			};
		public static Statistic[] Statistics = new Statistic[]
								{
									Statistic.Factory(
										"c2semispfc",
										TeamsSubscribes[0].Id,
										groupId:Groups[0].Id,
										position: 1,
										games: 1,
										points: 3,
										goalsScores:3,
										goalsAgainst:2,
										goalsDifference:1,
										lastfive: "win",
										won: 1
									),
									Statistic.Factory(
										"c2semiallitihad",
										TeamsSubscribes[2].Id,
										groupId:Groups[0].Id,
										position: 2,
										games: 1,
										goalsScores: 2,
										goalsAgainst: 3,
										goalsDifference: -1,
										lastfive: "win",
										lost: -1
									),
									Statistic.Factory(
										"c2semiliverpool",
										TeamsSubscribes[1].Id,
										groupId:Groups[1].Id
									),
									Statistic.Factory(
										"c2semisaprissa",
										TeamsSubscribes[3].Id,
										groupId:Groups[1].Id
									)
								};

		public static Match[] Matches = new Match[]{
			Match.Factory("c2semia", round: 1,groupId:Groups[0].Id, name: "Semi-Final",
					date: new DateTime(2005, 12, 14, 19, 20, 00), address: "Olimpico de Tokyo - Japão",
					homeId: TeamsSubscribes[2].Id, awayId: TeamsSubscribes[0].Id, finalGame: true,
					penalty: true, goalsHome:2, goalsAway:3),
			Match.Factory("c2semib", round: 1,groupId:Groups[1].Id, name: "Semi-Final",
					date: new DateTime(2005, 12, 15, 19, 20, 00), address: "Toyota - Japão",
					homeId: TeamsSubscribes[2].Id, awayId: TeamsSubscribes[0].Id, finalGame: true,
					penalty: true, goalsHome:2, goalsAway:3),
				Match.Factory("c2final", round: 1, groupId:Groups[2].Id, name: "Final",
					date: new DateTime(2005, 12, 17, 19, 20, 00), address: "Yokohama - Japão",
					vacancyHomeId: Vacancys[0].Id, vacancyAwayId: Vacancys[1].Id, finalGame: true,
					penalty: true),
		};
	}
}
