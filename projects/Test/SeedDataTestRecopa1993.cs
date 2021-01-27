using System;
using System.Linq;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;

namespace Keeper.Test
{
	public static class SeedData
	{
		public static Category[] Categorys = new Category[] {
							new Category("player1"),
							new Category("player2"),
							new Category("Profissional")
						};
		public static Championship Championship = Championship.Factory("c1",
				"Recopa Sulamericana", "1993", Categorys[2],
				Status.Finish);
		public static Team[] Teams = new Team[] {
							Team.Factory("t1","time1"),
							Team.Factory("t2","time2"),
							Team.Factory("t3","time3"),
							Team.Factory("t4","time4"),
							Team.Factory("t5","São Paulo","SAO","https://i.pinimg.com/originals/e2/76/12/e27612d24ece4b8e5f2fc4f61e2e5938.jpg"),
							Team.Factory("t6","Cruzeiro"),
							Team.Factory("t7","Delete"),
		};
		public static TeamSubscribe[] TeamsSubscribes = new TeamSubscribe[]
						{
					TeamSubscribe.Factory("ts1",Teams[4].Id,
							championshipId:Championship.Id,
							drowns: 2,
							games: 2,
							yellows: 4,
							status: Status.Champion),
					TeamSubscribe.Factory("ts2",Teams[5].Id,
							championshipId:Championship.Id,
							drowns: 2,
							games: 2,
							yellows: 2,
							status: Status.Eliminated)
						};
		public static Player[] Players = new Player[] {
							Player.Factory("p1","player1"),
							Player.Factory("p2","player2"),
							Player.Factory("p3","player3"),
							Player.Factory("p4","player4"),
							Player.Factory("p5","Palhinha"),
							Player.Factory("p6","Dinho"),
							Player.Factory("p7","Gilmar"),
							Player.Factory("p8","Guilherme"),
							Player.Factory("p9","Robson"),
							Player.Factory("p10","Rogério Lage"),
							Player.Factory("p11","Delete"),
						};
		public static PlayerSubscribe[] PlayersSubscribe = new PlayerSubscribe[]{
				PlayerSubscribe.Factory("ps1",Players[4].Id,
					TeamsSubscribes[0].Id, games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps2",Players[5].Id,
					TeamsSubscribes[0].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps3",Players[6].Id,
					TeamsSubscribes[0].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps4",Players[7].Id,
					TeamsSubscribes[0].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps5",Players[8].Id,
					TeamsSubscribes[1].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps6",Players[9].Id,
					TeamsSubscribes[1].Id,games:2, yellowCard:1),
			};
		public static Stage[] Stages = new Stage[]
				{
					Stage.Factory(
						"s1",
						Championship.Id,
						duplicateTurn:true,
						criterias: "0,1,2,3,4,5,6,7",
						name: "Final",
						order: 0,
						typeStage: TypeStage.Knockout,
						regulation: Classifieds.Configured
					)
				};

		public static Group[] Groups = new Group[]
		{
			Group.Factory("g1","Final", Stages[0].Id)
		};

		public static Vacancy[] Vacancys = new Vacancy[] {
				Vacancy.Factory("v1","Campeão da Libertadores 1993",Groups[0].Id, Classifieds.Configured),
				Vacancy.Factory("v2","Campeão da Supercopa 1993",Groups[0].Id, Classifieds.Configured),
			};
		public static Statistic[] Statistics = new Statistic[]
								{
									Statistic.Factory(
										"s1",
										TeamsSubscribes[0].Id,
										groupId:Groups[0].Id,
										reds: 0,
										drowns: 2,
										games: 2,
										goalsAgainst: 0,
										goalsDifference: 0,
										goalsScores: 0,
										lost: 0,
										won: 0,
										position: 1,
										lastfive: "draw,draw",
										rankMovement: 0,
										points: 2
									),
									Statistic.Factory(
										"s2",
										TeamsSubscribes[1].Id,
										groupId:Groups[0].Id,
										reds: 0,
										drowns: 2,
										games: 2,
										goalsAgainst: 0,
										goalsDifference: 0,
										goalsScores: 0,
										lost: 0,
										won: 0,
										yellows: 2,
										position: 2,
										lastfive: "draw,draw",
										rankMovement: 0,
										points: 2
									),
								};

		public static Match[] Matches = new Match[]{
			Match.Factory("m1", round: 1, status: Status.Finish,groupId:Groups[0].Id, name: "Final Ida"
				, date: new DateTime(1993, 9, 26, 21, 00, 00), address: "Morumbi - São Paulo/SP",
				aggregateGame: true, homeId: TeamsSubscribes[0].Id, vacancyHomeId: Vacancys[0].Id,
				vacancyAwayId: Vacancys[1].Id, awayId: TeamsSubscribes[1].Id,
				aggregateGoalsAway:0,aggregateGoalsHome:0,goalsHome:0,goalsAway:0),
				Match.Factory("m2", round: 1, groupId:Groups[0].Id, status: Status.Finish, name: "Final Volta"
				, date: new DateTime(1993, 9, 29, 21, 00, 00), address: "Mineirão - Belo Horizonte/MG",
				aggregateGame: true, homeId: TeamsSubscribes[1].Id, vacancyHomeId: Vacancys[1].Id,
				vacancyAwayId: Vacancys[0].Id, awayId: TeamsSubscribes[0].Id,finalGame: true,
				penalty: true, aggregateGoalsAway:0,aggregateGoalsHome:0,goalsHome:0,
				goalsAway:0,goalsPenaltyHome:2,goalsPenaltyAway:4),
		};
		public static EventGame[] EventGames = new EventGame[] {
					EventGame.Factory("e1",0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[0].Id
					),
					EventGame.Factory("e2",1,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[1].Id
					),
					EventGame.Factory("e3",2,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[2].Id
					),
					EventGame.Factory("e4",3,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[3].Id
					),
					EventGame.Factory("e5",4,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[4].Id
					),
					EventGame.Factory("e6",5,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						Matches[0].Id,
						PlayersSubscribe[5].Id
					)};

	}
}
