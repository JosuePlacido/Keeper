using Keeper.Domain.Enum;
using Keeper.Infrastructure.CrossCutting.DTO;
using System.Linq;

namespace Keeper.Test.DataExamples
{
	public static class ChampionshipCreateDTODataExamples
	{
		private static ChampionshipCreateDTO semiFinal = new ChampionshipCreateDTO
		{
			Name = "RECOPA",
			Edition = "Edicao",
			Category = SeedData.Categorys.First().Name,
			Stages = new StageDTO[] {
						new StageDTO
						{
							Criterias = new string[] { "pontos", "vitorias", "saldo de gol" },
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
								},
								new GroupDTO
								{
									Name = "Semi-Final 2",
								}
							}
						},
						new StageDTO
						{
							Criterias = new string[] { "pontos", "vitorias", "saldo de gol" },
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
									Vacancys = new VacancyDTO[] {
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
					TeamId = SeedData.Teams.Skip(0).First().Id,
					Players = SeedData.Players.Skip(0).Take(1).Select(p => p.Id).ToArray(),
					InitStageOrder = 0,
					InitGroupIndex = 0
				},
				new TeamDTO
				{
					TeamId = SeedData.Teams.Skip(1).First().Id,
					Players = SeedData.Players.Skip(1).Take(1).Select(p => p.Id).ToArray(),
					InitStageOrder = 0,
					InitGroupIndex = 0
				},
				new TeamDTO
				{
					TeamId = SeedData.Teams.Skip(2).First().Id,
					Players = SeedData.Players.Skip(2).Take(1).Select(p => p.Id).ToArray(),
					InitStageOrder = 0,
					InitGroupIndex = 1
				},
				new TeamDTO
				{
					TeamId = SeedData.Teams.Skip(3).First().Id,
					Players = SeedData.Players.Skip(3).Take(1).Select(p => p.Id).ToArray(),
					InitStageOrder = 0,
					InitGroupIndex = 1
				},
			}
		};

		public static ChampionshipCreateDTO SemiFinal { get => semiFinal; set => semiFinal = value; }
		public static ChampionshipCreateDTO SemiFinal1 { get => semiFinal; set => semiFinal = value; }
	}
}
