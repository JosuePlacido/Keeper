using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
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
							new Team("São Paulo"),
							new Team("Cruzeiro"),
		};

		public static Player[] Players = new Player[] {
							new Player("player1"),
							new Player("player2"),
							new Player("player3"),
							new Player("player4"),
							new Player("Palhinha"),
							new Player("Dinho"),
							new Player("Gilmar"),
							new Player("Guilherme"),
							new Player("Robson"),
							new Player("Rogério Lage"),
						};

		public static PlayerSubscribe[] PlayerSubscribes = new PlayerSubscribe[]{
							new PlayerSubscribe(Players[4].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[5].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[6].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[7].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[8].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[9].Id)
								.UpdateNumbers(2,0,1,0,0),
						};
		public static TeamSubscribe[] TeamsSubscribesNoPlayers = new TeamSubscribe[]
				{
					new TeamSubscribe(Teams[4].Id)
						.UpdateNumbers(
							reds: 0,
							drowns: 2,
							games: 2,
							goalsAgainst: 0,
							goalsDifference: 0,
							goalsScores: 0,
							lost: 0,
							won: 0,
							yellows: 4,
							status: Status.Champion),
					new TeamSubscribe(Teams[5].Id)
						.UpdateNumbers(
							reds: 0,
							drowns: 2,
							games: 2,
							goalsAgainst: 0,
							goalsDifference: 0,
							goalsScores: 0,
							lost: 0,
							won: 0,
							yellows: 2,
							status: Status.Champion),
				};
		public static Championship GetChampionship()
		{
			var players = new PlayerSubscribe[]{
							new PlayerSubscribe(Players[4].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[5].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[6].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[7].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[8].Id)
								.UpdateNumbers(2,0,1,0,0),
							new PlayerSubscribe(Players[9].Id)
								.UpdateNumbers(2,0,1,0,0),
						};
			var teams = new TeamSubscribe[]
				{
					new TeamSubscribe(Teams[4].Id)
						.AddPlayers(players.Skip(4).Take(4).ToArray())
						.UpdateNumbers(
							reds: 0,
							drowns: 2,
							games: 2,
							goalsAgainst: 0,
							goalsDifference: 0,
							goalsScores: 0,
							lost: 0,
							won: 0,
							yellows: 4,
							status: Status.Champion),
					new TeamSubscribe(Teams[5].Id)
						.AddPlayers(players.Skip(8).ToArray())
						.UpdateNumbers(
							reds: 0,
							drowns: 2,
							games: 2,
							goalsAgainst: 0,
							goalsDifference: 0,
							goalsScores: 0,
							lost: 0,
							won: 0,
							yellows: 2,
							status: Status.Champion)
				};
			var vacancys = new Vacancy[] {
				new Vacancy("Campeão da Libertadores 1993",Classifieds.Configured),
				new Vacancy("Campeão da Supercopa 1993",Classifieds.Configured)
			};
			var First = new Match(round: 1, status: Status.Finish, name: "Final Ida"
				, date: new DateTime(1993, 9, 26, 21, 00, 00), address: "Morumbi - São Paulo/SP",
				knockout: true, home: teams[0].Id, vacancyHome: vacancys[0].Id,
				vacancyAway: vacancys[1].Id, away: teams[1].Id);
			First.RegisterResult(0, 0,
				events: new EventGame[] {
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						players[0].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						players[1].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						players[2].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,true,
						players[3].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,false,
						players[4].Id
					),
					new EventGame(0,"Cartão Amarelo",
						TypeEvent.YellowCard,false,
						players[5].Id
					),
				});
			var Second = new Match(round: 2, status: Status.Finish, name: "Final Volta",
			 	date: new DateTime(1993, 9, 29, 21, 00, 00), address: "Mineirão - Belo Horizonte/MG",
				knockout: true, finalGame: true, penalty: true, home: teams[1].Id,
				vacancyHome: vacancys[1].Id, vacancyAway: vacancys[0].Id, away: teams[0].Id);
			Second.RegisterResult(0, 0, 2, 4);
			return new Championship("Recopa Sulamericana", "1993", Categorys[2].Id,
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
									new Statistic(teams[0].Id)
									.UpdateNumbers(
										reds: 0,
										drowns: 2,
										games: 2,
										goalsAgainst: 0,
										goalsDifference: 0,
										goalsScores: 0,
										lost: 0,
										won: 0,
										yellows: 4,
										position: 1,
										lastfive: "draw,draw",
										rankMovement: RankMovement.Stay,
										points: 2
									),
									new Statistic(teams[1].Id)
									.UpdateNumbers(
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
										rankMovement: RankMovement.Stay,
										points: 2
									),
								}
							).AddMatches(new Match[]{
								First,Second
							})
						}
					)
				},
				teams
				);
		}
	}
}
