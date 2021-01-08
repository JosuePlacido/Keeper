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
		public static Championship GetChampionship()
		{
			var First = new Match(1, name: "Final Ida", status: Status.Finish
				, date: new DateTime(1993, 9, 26, 21, 00, 00), address: "Morumbi - São Paulo/SP",
				knockout: true)
			{
				VacancyAwayId = "10600747-5b0a-4929-925a-b6b13c1d08a3",
				VacancyHomeId = "ed396baa-df8d-40bc-8710-82bc11206d7d",
				AwayId = "63d211cc-ef0a-41bf-b592-254fcd45190b",
				HomeId = "7008b863-ec07-48ba-9d42-bad88f96bb8a",
				EventGames = new EventGame[] {
											new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 0,
												Type = TypeEvent.YellowCard,
												RegisterPlayerId = "1d964d8e-3413-4b55-a97c-ed8d155e62f1"
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 1,
												Type = TypeEvent.YellowCard,
												RegisterPlayerId = "fc719893-afb9-4aa6-ba05-287b5ac6d187"
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 2,
												RegisterPlayerId = "df90fc6a-7442-4850-8b38-20aa0c3e2641",
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 3,
												Type = TypeEvent.YellowCard,
												RegisterPlayerId = "d6dd8f41-d549-41b5-a494-979857f43c4b"
											},new EventGame
											{
												RegisterPlayerId = "66ffc64c-61fa-4b59-8477-724b6bc17d6f",
												Description = "Cartão Amarelo",
												IsHomeEvent = false,
												Order = 4,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												RegisterPlayerId = "82eac818-3cae-4be4-bc16-890279599df8",
												Description = "Cartão Amarelo",
												IsHomeEvent = false,
												Order = 5,
												Type = TypeEvent.YellowCard
											}
									}
			};
			First.RegisterResult(0, 0);
			var Second = new Match(2, name: "Final volta", status: Status.Finish
				, date: new DateTime(1993, 9, 29, 21, 00, 00), address: "Mineirão - Belo Horizonte/MG",
				knockout: true, finalGame: true, penalty: true)
			{
				VacancyHomeId = "10600747-5b0a-4929-925a-b6b13c1d08a3",
				VacancyAwayId = "ed396baa-df8d-40bc-8710-82bc11206d7d",
				HomeId = "63d211cc-ef0a-41bf-b592-254fcd45190b",
				AwayId = "7008b863-ec07-48ba-9d42-bad88f96bb8a",
				EventGames = new EventGame[0]
			};
			Second.RegisterResult(0, 0, 2, 4);
			return new Championship()
			{
				Name = "Recopa Sulamericana",
				Edition = "1993",
				Status = Status.Finish,
				Category = new Category()
				{
					Name = "Profissional",

				},
				Stages = new Stage[]
				{
					new Stage()
					{
						Criterias = "0,1,2,3,4,5,6,7",
						Name = "Final",
						Order = 0,
						TypeStage = TypeStage.Knockout,
						Regulation = Classifieds.Configured,
						Groups = new Group[]
						{
							new Group
							{
								Id = "8ea2bb9f-2698-4faf-92c6-c0165ce110fc",
								Name = "Final",
								Matchs = new Match[]{First,Second},
								Vacancys = new Vacancy[]
								{
									new Vacancy
									{
										Id = "ed396baa-df8d-40bc-8710-82bc11206d7d",
										Description = "Campeão da Libertadores 1993",
										OcupationType = Classifieds.Configured,
									},
									new Vacancy
									{
										Id = "10600747-5b0a-4929-925a-b6b13c1d08a3",
										Description = "Campeão da Supercopa 1993",
										OcupationType = Classifieds.Configured
									}
								},
								Statistics = new Statistics[]
								{
									new Statistics
									{
										Reds = 0,
										Drowns = 2,
										Games = 2,
										GoalsAgainst = 0,
										GoalsDifference = 0,
										GoalsScores = 0,
										Lost = 0,
										Won = 0,
										Yellows = 4,
										Position = 1,
										Lastfive = "draw,draw",
										RankMovement = RankMovement.Stay,
										Points = 2,
										TeamSubscribeId = "7008b863-ec07-48ba-9d42-bad88f96bb8a"
									},
									new Statistics
									{
										Reds = 0,
										Drowns = 2,
										Games = 2,
										GoalsAgainst = 0,
										GoalsDifference = 0,
										GoalsScores = 0,
										Lost = 0,
										Won = 0,
										Yellows = 2,
										Position = 2,
										Lastfive = "draw,draw",
										RankMovement = RankMovement.Stay,
										Points = 2,
										TeamSubscribeId = "63d211cc-ef0a-41bf-b592-254fcd45190b"
									}
								},
							}
						}
					}
				},
				Teams = new TeamSubscribe[]
				{
					new TeamSubscribe
					{
						Id = "7008b863-ec07-48ba-9d42-bad88f96bb8a",
						Players = new PlayerSubscribe[]{
							new PlayerSubscribe
							{
								Id = "1d964d8e-3413-4b55-a97c-ed8d155e62f1",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Palhinha"
								}
							},
							new PlayerSubscribe
							{
								Id = "fc719893-afb9-4aa6-ba05-287b5ac6d187",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Gilmar"
								}
							},
							new PlayerSubscribe
							{
								Id = "df90fc6a-7442-4850-8b38-20aa0c3e2641",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Guilherme"
								}
							},
							new PlayerSubscribe
							{
								Id = "d6dd8f41-d549-41b5-a494-979857f43c4b",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Dinho"
								}
							},
						},
						Reds = 0,
						Drowns = 2,
						Games = 2,
						GoalsAgainst = 0,
						GoalsDifference = 0,
						GoalsScores = 0,
						Lost = 0,
						Won = 0,
						Yellows = 4,
						Status = Status.Champion,
						Team = new Team
						{
							Name = "São Paulo"
						}
					},
					new TeamSubscribe
					{
						Id = "63d211cc-ef0a-41bf-b592-254fcd45190b",
						Players = new PlayerSubscribe[]{
							new PlayerSubscribe
							{
								Id = "66ffc64c-61fa-4b59-8477-724b6bc17d6f",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Robson"
								}
							},
							new PlayerSubscribe
							{
								Id = "82eac818-3cae-4be4-bc16-890279599df8",
								Games = 2,
								YellowCard = 1,
								Player = new Player
								{
									Name = "Rogério Lage"
								}
							},
						},
						Reds = 0,
						Drowns = 2,
						Games = 2,
						GoalsAgainst = 0,
						GoalsDifference = 0,
						GoalsScores = 0,
						Lost = 0,
						Won = 0,
						Yellows = 2,
						Status = Status.Eliminated,
						Team = new Team
						{
							Name = "Cruzeiro"
						}
					},
				},
			};
		}
	}
}
