using Xunit;
using Xunit.Abstractions;
using AutoMapper;
using Newtonsoft.Json;
using System.Linq;
using Keeper.Application.DTO;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Test.DataExamples;
using Keeper.Application.Services;
using FluentValidation.Results;

namespace Keeper.Test.Integration.Application
{
	public class CreateChampionship : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public CreateChampionship(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);

		[Fact]
		public void TestCreateChampionship()
		{
			ValidationResult result = null;
			using (var context = Fixture.CreateContext())
			{
				var repo = new ChampionshipRepository(context);
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<ChampionshipDTOToDomainProfile>();
					cfg.AddProfile<MatchEditProfile>();
				});
				var mapper = config.CreateMapper();
				var test = ChampionshipCreateDTODataExamples.SemiFinal;
				result = new ChampionshipService(mapper, repo).Create(test).Result;
			}
			var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
			Assert.NotNull(result);
			Assert.True(result.IsValid);
		}
	}
}
