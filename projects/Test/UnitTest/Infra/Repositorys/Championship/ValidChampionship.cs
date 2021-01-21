using System.Linq;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Keeper.Test;
using Xunit;

namespace Keeper.Test.UnitTest.Infra.Repositorys
{
	internal class ValidChampionshipSetup : TheoryData<Championship>
	{
		public ValidChampionshipSetup()
		{
			Add(new Championship("test", "edition", SeedData.Categorys.First(), Status.Created));
		}
	}
}
