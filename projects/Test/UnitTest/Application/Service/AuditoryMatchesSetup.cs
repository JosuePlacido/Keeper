
using Domain.Models;
using Infrastructure.CrossCutting.DTO;
using Xunit;

namespace Test.UnitTest.Application.Service
{
	public class AuditoryMatchesTestModel
	{
		public MatchEditsScope Case { get; set; }
		public int ExpectedErrorsCount { get; set; }
		public string[] ExpectedIdsMatchesWithError { get; set; }

	}
	public class AuditoryMatchesSetup : TheoryData<AuditoryMatchesTestModel>
	{
		public AuditoryMatchesSetup()
		{
			Add(new AuditoryMatchesTestModel
			{
				ExpectedErrorsCount = 4,
				ExpectedIdsMatchesWithError = new string[] { "1", "2" },
				Case = new MatchEditsScope
				{
					Stages = new MatchStageEdit[]{
						new MatchStageEdit{
							Id = "1",
							DuplicateTurn = false,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "1",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 1,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "2",
											Round = 1,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "3",
											Round = 2,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "4",
											Round = 2,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										}

									}
								}
							}
						}
					}
				}
			});
			Add(new AuditoryMatchesTestModel
			{
				ExpectedErrorsCount = 2,
				ExpectedIdsMatchesWithError = new string[] { "1", "3", "5" },
				Case = new MatchEditsScope
				{
					Stages = new MatchStageEdit[]{
						new MatchStageEdit{
							Id = "1",
							DuplicateTurn = false,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "1",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 1,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "2",
											Round = 1,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},
										new MatchItemDTO{
											Id = "3",
											Round = 2,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},
										new MatchItemDTO{
											Id = "4",
											Round = 2,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "5",
											Round = 3,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},
										new MatchItemDTO{
											Id = "6",
											Round = 3,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										}

									}
								}
							}
						}
					}
				}
			});

			Add(new AuditoryMatchesTestModel
			{
				ExpectedErrorsCount = 2,
				ExpectedIdsMatchesWithError = new string[0],
				Case = new MatchEditsScope
				{
					Stages = new MatchStageEdit[]{
						new MatchStageEdit{
							Id = "1",
							DuplicateTurn = true,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "1",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 3,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "2",
											Round = 3,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},

										new MatchItemDTO{
											Id = "5",
											Round = 2,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "6",
											Round = 2,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "3",
											Round = 1,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "4",
											Round = 1,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "7",
											Round = 4,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "8",
											Round = 4,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "9",
											Round = 5,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},
										new MatchItemDTO{
											Id = "10",
											Round = 5,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},

										new MatchItemDTO{
											Id = "11",
											Round = 6,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},

									}
								}
							}
						}
					}
				}
			});
			Add(new AuditoryMatchesTestModel
			{
				ExpectedErrorsCount = 0,
				ExpectedIdsMatchesWithError = new string[0],
				Case = new MatchEditsScope
				{
					Stages = new MatchStageEdit[]{
						new MatchStageEdit{
							Id = "1",
							DuplicateTurn = true,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "1",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 3,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "2",
											Round = 3,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},

										new MatchItemDTO{
											Id = "5",
											Round = 2,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "6",
											Round = 2,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "3",
											Round = 1,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "4",
											Round = 1,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "7",
											Round = 4,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "8",
											Round = 4,
											HomeId = "DDD",
											Home = new MatchEditTeam{
												Team = "DDD"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										},

										new MatchItemDTO{
											Id = "9",
											Round = 5,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},
										new MatchItemDTO{
											Id = "10",
											Round = 5,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},

										new MatchItemDTO{
											Id = "11",
											Round = 6,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "DDD",
											Away = new MatchEditTeam{
												Team = "DDD"
											},
										},
										new MatchItemDTO{
											Id = "12",
											Round = 6,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										}
									}
								}
							}
						},
						new MatchStageEdit{
							Id = "2",
							DuplicateTurn = true,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "2",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 1,
											VacancyAwayId = "1",
											VacancyAway = new MatchEditVacancy{
												Description = "1° colocado"
											},
											VacancyHomeId = "2",
											VacancyHome = new MatchEditVacancy{
												Description = "2° colocado"
											}
										},
										new MatchItemDTO{
											Id = "2",
											Round = 1,
											VacancyHomeId = "1",
											VacancyHome = new MatchEditVacancy{
												Description = "1° colocado"
											},
											VacancyAwayId = "2",
											VacancyAway = new MatchEditVacancy{
												Description = "2° colocado"
											}
										},
									}
								}
							}
						},

					}
				}
			});
		}
	}
}
