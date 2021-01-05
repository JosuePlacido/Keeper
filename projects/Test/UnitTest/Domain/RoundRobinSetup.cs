using Domain.Models;
using Xunit;

namespace Test.Domain
{
	internal class RoundRobinSetup : TheoryData<Group, bool, bool>
	{
		public RoundRobinSetup()
		{
			var TeamPair = new Group()
			{
				Statistics = new Statistics[]{
					new Statistics{
						TeamSubscribeId = "AAA",
					},
					new Statistics{
						TeamSubscribeId = "BBB",
					},
				}
			};
			var TeamNotPair = new Group()
			{
				Statistics = new Statistics[]{
					new Statistics{
						TeamSubscribeId = "AAA",
					},
					new Statistics{
						TeamSubscribeId = "BBB",
					},
					new Statistics{
						TeamSubscribeId = "CCC",
					},
				}
			};
			var VacancyPair = new Group()
			{
				Vacancys = new Vacancy[]{
					new Vacancy{
						Id = "ZZZ"
					},
					new Vacancy{
						Id = "XXX"
					},
					new Vacancy{
						Id = "YYY"
					},
					new Vacancy{
						Id = "WWW"
					},
				}
			};
			var VacancyNotPair = new Group()
			{
				Vacancys = new Vacancy[]{
					new Vacancy{
						Id = "ZZZ"
					},
					new Vacancy{
						Id = "XXX"
					},
					new Vacancy{
						Id = "YYY"
					},
					new Vacancy{
						Id = "WWW"
					},
					new Vacancy{
						Id = "VVV"
					},
				}
			};
			var HybridPair = new Group()
			{
				Statistics = new Statistics[]{
					new Statistics{
						TeamSubscribeId = "AAA",
					},
					new Statistics{
						TeamSubscribeId = "BBB",
					},
					new Statistics{
						TeamSubscribeId = "CCC",
					},
					new Statistics{
						TeamSubscribeId = "DDD",
					},
				},
				Vacancys = new Vacancy[]{
					new Vacancy{
						Id = "ZZZ"
					},
					new Vacancy{
						Id = "XXX"
					},
					new Vacancy{
						Id = "YYY"
					},
					new Vacancy{
						Id = "WWW"
					},
				}
			};
			var HybridNotPair = new Group()
			{
				Statistics = new Statistics[]{
					new Statistics{
						TeamSubscribeId = "AAA",
					},
					new Statistics{
						TeamSubscribeId = "BBB",
					},
					new Statistics{
						TeamSubscribeId = "CCC",
					},
				},
				Vacancys = new Vacancy[]{
					new Vacancy{
						Id = "YYY"
					},
					new Vacancy{
						Id = "WWW"
					},
				}
			};

			//Quantidade par com times
			Add(TeamPair, false, false);
			Add(TeamPair, true, false);
			Add(TeamPair, true, true);
			//Quantidade impar com times
			//Add(TeamNotPair, false, false);
			Add(TeamNotPair, true, false);
			Add(TeamNotPair, true, true);
			//Quantidade par com vagas
			Add(VacancyPair, false, false);
			Add(VacancyPair, true, false);
			Add(VacancyPair, true, true);
			//Quantidade impar com vagas
			Add(VacancyNotPair, false, false);
			Add(VacancyNotPair, true, false);
			Add(VacancyNotPair, true, true);
			//Quantidade par com times e vagas
			Add(HybridPair, false, false);
			Add(HybridPair, true, false);
			Add(HybridPair, true, true);
			//Quantidade impar com times e vagas
			Add(HybridNotPair, false, false);
			Add(HybridNotPair, true, false);
			Add(HybridNotPair, true, true);
		}
	}
}
