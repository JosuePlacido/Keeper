
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Test.UnitTest.Application.Service
{
	public class TestAuditoryMatches
	{
		private readonly ITestOutputHelper _output;
		public TestAuditoryMatches(ITestOutputHelper output)
			=> _output = output;

		[Theory]
		[ClassData(typeof(AuditoryMatchesSetup))]
		public void TestAuditoryMatchesList(AuditoryMatchesTestModel test)
		{
			var result = new ChampionshipService(null, null, null, null, null, null, null, null)
				.CheckMatches(test.Case);
			Assert.True(test.ExpectedErrorsCount == result.Errors.Count);
			var idsMatchesWithError = result.Stages.SelectMany(stg => stg.Groups.SelectMany(
				grp => grp.Matchs.Where(MatchAuditoryContants => MatchAuditoryContants.HasError)
					.Select(mtc => mtc.Id)
			)).ToArray();
			Assert.Equal(test.ExpectedIdsMatchesWithError, idsMatchesWithError);
		}
	}
}
