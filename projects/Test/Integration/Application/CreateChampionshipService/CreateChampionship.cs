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
using Keeper.Infrastructure.DAO;
using Keeper.Application.Contract;
using Keeper.Application.Services.CreateChampionship;
using Keeper.Infrastructure.Data;

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
			IServiceResponse result = null;
			using (var transaction = Fixture.Connection.BeginTransaction())
			{
				using (var context = Fixture.CreateContext(transaction))
				{
					var repo = new ChampionshipRepository(context);
					var config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<ChampionshipDTOToDomainProfile>();
						cfg.AddProfile<MatchEditProfile>();
					});
					var mapper = config.CreateMapper();
					var test = ChampionshipCreateDTODataExamples.SemiFinal;
					result = new ChampionshipService(mapper, new UnitOfWork(context, null))
						.Create(test).Result;
				}
			}
			var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
			Assert.NotNull(result);
			Assert.True(result.ValidationResult.IsValid);
		}
	}
}
