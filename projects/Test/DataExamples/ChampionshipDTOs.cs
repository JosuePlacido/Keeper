using Application.DTO;
using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataExamples
{
	public static class ChampionshipCreateDTODataExamples
	{
		private static ChampionshipCreateDTOModelTest[] ValidCreate = new ChampionshipCreateDTOModelTest[] {
			new ChampionshipCreateDTOModelTest
			{
				Enter = new ChampionshipCreateDTO() {
					Name = "RECOPA",
					Edition = "Edicao",
					CategoryId = "Category",
					Stages = new StageDTO[]{
						new StageDTO
						{
							Criterias = new string[]{"pontos","vitorias","saldo de gol"},
							TypeStage = TypeStage.Knockout,
							DuplicateTurn = true,
							MirrorTurn = true,
							Name = "Final",
							Order = 0,
							Regulation = Classifieds.Configured,
							Groups = new GroupDTO[]
							{
								new GroupDTO
								{
									Name = "Final",
									Teams = new string[]{"AAA","BBB"}
								}
							}
						}
					},
					Teams = new TeamDTO[]{
						new TeamDTO
						{
							TeamId = "AAA",
							Players = new string[]{"Fulano","Beltrano"}
						},
						new TeamDTO
						{
							TeamId = "BBB",
							Players = new string[]{"Cicrano","Trodano"}
						},
					}
				},
				Expected = new Championship
				{
					Name = "RECOPA",
					Edition = "Edicao",
					CategoryId = "Category",
					Status = Status.Created,
					Stages = new Stage[]{
						new Stage
						{
							Criterias = "pontos,vitorias,saldo de gol",
							TypeStage = TypeStage.Knockout,
							Name = "Final",
							Order = 0,
							Regulation = Classifieds.Configured,							
							Groups = new Group[]
							{
								new Group
								{
									Vacancys = new Vacancy[0],
									Name = "Final",
									Statistics = new Statistics[]
									{
										new Statistics
										{
											TeamSubscribeId = "AAA"
										},
										new Statistics
										{
											TeamSubscribeId = "BBB"
										}
									}
								}
							}
						}
					},				
					Teams = new TeamSubscribe[]
					{
						new TeamSubscribe
						{
							TeamId = "AAA",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Fulano"
								},
								new PlayerSubscribe
								{
									PlayerId = "Beltrano"
								}
							}
						},
						new TeamSubscribe
						{
							TeamId = "BBB",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Cicrano"
								},
								new PlayerSubscribe
								{
									PlayerId = "Trodano"
								}
							}
						},
					}
				},
			},
			new ChampionshipCreateDTOModelTest
			{
				Enter = new ChampionshipCreateDTO() {
					Name = "RECOPA",
					Edition = "Edicao",
					CategoryId = "Category",
					Stages = new StageDTO[]{
						new StageDTO
						{
							Criterias = new string[]{"pontos","vitorias","saldo de gol"},
							TypeStage = TypeStage.Knockout,
							DuplicateTurn = true,
							MirrorTurn = true,
							Name = "Semi-Final",
							Order = 0,
							Regulation = Classifieds.Configured,
							Groups = new GroupDTO[]
							{
								new GroupDTO
								{
									Name = "Semi-Final 1",
									Teams = new string[]{"AAA","BBB"}
								},
								new GroupDTO
								{
									Name = "Semi-Final 2",
									Teams = new string[]{"CCC","DDD"}
								}
							}
						},
						new StageDTO
						{
							Criterias = new string[]{"pontos","vitorias","saldo de gol"},
							TypeStage = TypeStage.Knockout,
							DuplicateTurn = true,
							MirrorTurn = true,
							Name = "Final",
							Order = 1,
							Regulation = Classifieds.Configured,
							Groups = new GroupDTO[]
							{
								new GroupDTO
								{
									Name = "Final",
									Vacancys = new VacancyDTO[]{
										new VacancyDTO
										{
											FromStageIndex = 0,
											FromGroupIndex = 0,
											Description = "Vencedor da Semifinal 1",
											FromPosition = 1,
											OcupationType = Classifieds.Configured
										},
										new VacancyDTO
										{
											FromStageIndex = 0,
											FromGroupIndex = 1,
											Description = "Vencedor da Semifinal 2",
											FromPosition = 1,
											OcupationType = Classifieds.Configured
										},
									}
								}
							}
						}
					},
					Teams = new TeamDTO[]{
						new TeamDTO
						{
							TeamId = "AAA",
							Players = new string[]{"Fulano" }
						},
						new TeamDTO
						{
							TeamId = "BBB",
							Players = new string[]{ "Beltrano" }
						},
						new TeamDTO
						{
							TeamId = "CCC",
							Players = new string[]{ "Cicrano" }
						},
						new TeamDTO
						{
							TeamId = "DDD",
							Players = new string[]{"Trodano"}
						},
					}
				},
				Expected = new Championship
				{
					Name = "RECOPA",
					Edition = "Edicao",
					CategoryId = "Category",
					Status = Status.Created,
					Stages = new Stage[]{
						new Stage
						{
							Criterias = "pontos,vitorias,saldo de gol",
							TypeStage = TypeStage.Knockout,
							Name = "Semi-Final",
							Order = 0,
							Regulation = Classifieds.Configured,
							Groups = new Group[]
							{
								new Group
								{
									Vacancys = new Vacancy[0],
									Name = "Semi-Final 1",
									Statistics = new Statistics[]
									{
										new Statistics
										{
											TeamSubscribeId = "AAA"
										},
										new Statistics
										{
											TeamSubscribeId = "BBB"
										}
									}
								},
								new Group
								{
									Vacancys = new Vacancy[0],
									Name = "Semi-Final 2",
									Statistics = new Statistics[]
									{
										new Statistics
										{
											TeamSubscribeId = "CCC"
										},
										new Statistics
										{
											TeamSubscribeId = "DDD"
										}
									}
								}
							}
						},
						new Stage
						{
							Criterias = "pontos,vitorias,saldo de gol",
							TypeStage = TypeStage.Knockout,
							Name = "Final",
							Order = 1,
							Regulation = Classifieds.Configured,							
							Groups = new Group[]
							{
								new Group
								{
									Name = "Final",
									Statistics = new Statistics[0],
									Vacancys = new Vacancy[]
									{
										new Vacancy
										{
											FromStageOrder = 0,
											Description = "Vencedor da Semifinal 1",
											FromPosition = 1,
											OcupationType = Classifieds.Configured
										},
										new Vacancy
										{
											FromStageOrder = 0,
											Description = "Vencedor da Semifinal 2",
											FromPosition = 1,
											OcupationType = Classifieds.Configured
										},
									}
								}
							}
						}
					},
					Teams = new TeamSubscribe[]
					{
						new TeamSubscribe
						{
							TeamId = "AAA",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Fulano"
								}
							}
						},
						new TeamSubscribe
						{
							TeamId = "BBB",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Beltrano"
								}
							}
						},
						new TeamSubscribe
						{
							TeamId = "CCC",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Cicrano"
								}
							}
						},
						new TeamSubscribe
						{
							TeamId = "DDD",
							Status = Status.Matching,
							Players = new PlayerSubscribe[]
							{
								new PlayerSubscribe
								{
									PlayerId = "Trodano"
								}
							}
						},
					}
				},
			}
			
		};

		public static ChampionshipCreateDTOModelTest[] GetValid()
		{
			return ValidCreate;
		}
	}
	public class ChampionshipCreateDTOModelTest
	{
		public ChampionshipCreateDTO Enter { get; set; }
		public Championship Expected { get; set; }
	}

}
