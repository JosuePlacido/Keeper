using System.Linq;
using Domain.Enum;
using Domain.Models;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidChampionshipSetup : TheoryData<Championship>
	{
		public ValidChampionshipSetup()
		{
			Add(Championship.Factory("test", "test", "edition",
				SeedData.Categorys.First(), Status.Created));
		}
	}
}
