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
		public static Team[] Teams = new Team[] {
							new Team("time1"),
							new Team("time2"),
							new Team("time3"),
							new Team("time4"),
							new Team("São Paulo","SAO","https://i.pinimg.com/originals/e2/76/12/e27612d24ece4b8e5f2fc4f61e2e5938.jpg"),
							new Team("Cruzeiro"),
							new Team("Delete"),
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
				PlayerSubscribe.Factory("ps1",
					playerId:Players[4].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps2",
					playerId:Players[5].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps3",
					playerId:Players[6].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps4",
					playerId:Players[7].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps5",
					playerId:Players[8].Id,games:2, yellowCard:1),
				PlayerSubscribe.Factory("ps6",
					playerId:Players[9].Id,games:2, yellowCard:1),
			};
		public static TeamSubscribe[] TeamsSubscribes = new TeamSubscribe[]
				{
					TeamSubscribe.Factory("ts1",Teams[4].Id,
							drowns: 2,
							games: 2,
							yellows: 4,
							status: Status.Champion,
							players:PlayersSubscribe.Take(4).ToList()),
					TeamSubscribe.Factory("ts2",Teams[5].Id,
							drowns: 2,
							games: 2,
							yellows: 2,
							status: Status.Eliminated,
							players:PlayersSubscribe.Skip(4).ToList())
				};
		public static Championship GetChampionship()
		{
			var vacancys = new Vacancy[] {
				new Vacancy("Campeão da Libertadores 1993",Classifieds.Configured),
				new Vacancy("Campeão da Supercopa 1993",Classifieds.Configured)
			};
			var First = new Match(round: 1, status: Status.Finish, name: "Final Ida"
				, date: new DateTime(1993, 9, 26, 21, 00, 00), address: "Morumbi - São Paulo/SP",
				knockout: true, home: TeamsSubscribes[0].Id, vacancyHome: vacancys[0].Id,
				vacancyAway: vacancys[1].Id, away: TeamsSubscribes[1].Id);
			First.RegisterResult(0, 0,
				events: new EventGame[] {
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						PlayersSubscribe[0].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						PlayersSubscribe[1].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						PlayersSubscribe[2].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						PlayersSubscribe[3].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,false,
						PlayersSubscribe[4].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,false,
						PlayersSubscribe[5].Id
					),
				});
			var Second = new Match(round: 2, status: Status.Finish, name: "Final Volta",
			 	date: new DateTime(1993, 9, 29, 21, 00, 00), address: "Mineirão - Belo Horizonte/MG",
				knockout: true, finalGame: true, penalty: true, home: TeamsSubscribes[1].Id,
				vacancyHome: vacancys[1].Id, vacancyAway: vacancys[0].Id, away: TeamsSubscribes[0].Id);
			Second.RegisterResult(0, 0, 2, 4);
			return new Championship("Recopa Sulamericana", "1993", Categorys[2],
				Status.Finish,
				new Stage[]
				{
					new Stage(
						mirrorTurn:false,
						duplicateTurn:true,
						criterias: "0,1,2,3,4,5,6,7",
						name: "Final",
						order: 0,
						typeStage: TypeStage.Knockout,
						regulation: Classifieds.Configured,
						groups: new Group[]
						{
							new Group(name:"Final", vacancys: vacancys,
							stats: new Statistic[]
								{
									Statistic.Factory(
										"stat1",
										TeamsSubscribes[0].Id,
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
										"stat2",
										TeamsSubscribes[1].Id,
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
								}
							).AddMatches(new Match[]{
								First,Second
							})
						}
					)
				},
				TeamsSubscribes
				);
		}
	}
}
