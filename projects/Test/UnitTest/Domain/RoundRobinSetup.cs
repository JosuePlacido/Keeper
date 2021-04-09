using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Xunit;

namespace Keeper.Test.Domain
{
	internal class RoundRobinSetup : TheoryData<Group, bool, bool>
	{
		public RoundRobinSetup()
		{
			var TeamPair = Group.Factory(null, "TeamPair", null, statistics: new Statistic[]{
					new Statistic("AAA",""),
					new Statistic("BBB","")
				});
			var TeamNotPair = Group.Factory(null, "TeamNotPair", null, statistics: new Statistic[]{
					new Statistic("AAA",""),
					new Statistic("BBB",""),
					new Statistic("CCC",""),
				});
			var VacancyPair = Group.Factory(null, "VacancyPair", null, statistics: new Statistic[0],
				vacancys: new Vacancy[]{
					new Vacancy("ZZZ", Classifieds.BestVsWorst),
					new Vacancy("XXX", Classifieds.BestVsWorst),
					new Vacancy("YYY", Classifieds.BestVsWorst),
					new Vacancy("WWW", Classifieds.BestVsWorst),
				}
			);
			var VacancyNotPair = Group.Factory(null, "VacancyNotPair", null, statistics: new Statistic[0],
				vacancys: new Vacancy[]{
					new Vacancy("ZZZ", Classifieds.BestVsWorst),
					new Vacancy("XXX", Classifieds.BestVsWorst),
					new Vacancy("YYY", Classifieds.BestVsWorst),
					new Vacancy("WWW", Classifieds.BestVsWorst),
					new Vacancy("VVV", Classifieds.BestVsWorst),
				}
			);
			var HybridPair = Group.Factory(null, "HybridPair", null,
				statistics: new Statistic[]{
					new Statistic("AAA",""),
					new Statistic("BBB",""),
					new Statistic("CCC",""),
					new Statistic("DDD",""),
				},
				vacancys: new Vacancy[]{
					new Vacancy("ZZZ", Classifieds.BestVsWorst),
					new Vacancy("XXX", Classifieds.BestVsWorst),
					new Vacancy("YYY", Classifieds.BestVsWorst),
					new Vacancy("WWW", Classifieds.BestVsWorst),
				}
			);
			var HybridNotPair = Group.Factory(null, "HybridNotPair", null,
				statistics: new Statistic[]{
					new Statistic("AAA",""),
					new Statistic("BBB",""),
					new Statistic("CCC",""),
					new Statistic("DDD",""),
					new Statistic("EEE",""),
				},
				vacancys: new Vacancy[]{
					new Vacancy("ZZZ", Classifieds.BestVsWorst),
					new Vacancy("XXX", Classifieds.BestVsWorst),
					new Vacancy("YYY", Classifieds.BestVsWorst),
					new Vacancy("WWW", Classifieds.BestVsWorst),
				}
			);

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
