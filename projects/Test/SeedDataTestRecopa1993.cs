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
						IsDoubleTurn = true,
						Name = "Final",
						Order = 0,
						SpotsNextStage = 0,
						TypeStage = TypeStage.Knockout,
						Regulation = Classifieds.Configured,
						Teams = 2,
						Groups = new Group[]
						{
							new Group
							{
								Name = "Final",
								Teams = new TeamSubscribe[]
								{
									new TeamSubscribe
									{
										Players = new PlayerSubscribe[]{
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Palhinha"
												}
											},
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Gilmar"
												}
											},
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Guilherme"
												}
											},
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Dinho"
												}
											},
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
												Lastfive = "draw,draw",
												Lost = 0,
												Position = 1,
												RankMovement = RankMovement.Stay,
												Won = 0,
												Points = 2,
												Yellows = 4,

											}
										},
										Status = Status.Champion,
										Team = new Team
										{
											Name = "São Paulo"
										}
									},
									new TeamSubscribe
									{
										Players = new PlayerSubscribe[]{
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Robson"
												}
											},
											new PlayerSubscribe
											{
												Games = 2,
												YellowCard = 1,
												Player = new Player
												{
													Name = "Rogério Lage"
												}
											},
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
												Lastfive = "draw,draw",
												Lost = 0,
												Position = 2,
												RankMovement = RankMovement.Stay,
												Won = 0,
												Points = 2,
												Yellows = 2,
											}
										},
										Status = Status.Eliminated,
										Team = new Team
										{
											Name = "Cruzeiro"
										}
									},
								},
								Matchs = new Match[]{
									new Match
									{
										AggregateGame = true,
										Local = "Morumbi - São Paulo/SP",
										AggregateGoalsAway = 0,
										AggregateGoalsHome = 0,
										Date = new DateTime(1993,9,26,21,00,00),
										Name = "Final Ida",
										GoalsAway = 0,
										FinalGame = false,
										GoalsHome = 0,
										Penalty = false,
										Round = 1,
										Status = Status.Finish,
										EventGames = new EventGame[] {
											new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 0,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 1,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 2,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = true,
												Order = 3,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = false,
												Order = 4,
												Type = TypeEvent.YellowCard
											},new EventGame
											{
												Description = "Cartão Amarelo",
												IsHomeEvent = false,
												Order = 5,
												Type = TypeEvent.YellowCard
											}
									}
									},
									new Match()
									{
										AggregateGame = true,
										Local = "Mineirão - Belo Horizonte/MG",
										AggregateGoalsAway = 0,
										AggregateGoalsHome = 0,
										Date = new DateTime(1993,9,29,21,00,00),
										Name = "Final Volta",
										GoalsAway = 0,
										FinalGame = true,
										GoalsHome = 0,
										Penalty = true,
										Round = 1,
										Status = Status.Finish,
										GoalsPenaltyHome = 2,
										GoalsPenaltyVisitante = 4,
										EventGames = new EventGame[0]
									}
								},
								Vacancys = new Vacancy[]
								{
									new Vacancy
									{
										Description = "Campeão da Libertadores 1993",
										OcupationType = Classifieds.Configured
									},
									new Vacancy
									{
										Description = "Campeão da Supercopa 1993",
										OcupationType = Classifieds.Configured
									}
								}
							}
						}
					}
				}

			};
		}
	}
}
