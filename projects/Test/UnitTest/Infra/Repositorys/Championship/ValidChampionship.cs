using System.Linq;
using Domain.Enum;
using Domain.Models;
using Test.DataExamples;
using Xunit;

namespace Test.UnitTest.Infra.Repositorys
{
	internal class ValidChampionshipSetup : TheoryData<Championship>
	{
		public ValidChampionshipSetup()
		{
			Add(new Championship("test", "edition", SeedData.Categorys.First().Id, Status.Created));
		}
	}
}
