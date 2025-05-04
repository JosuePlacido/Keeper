
using AutoMapper;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.Repository;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.EditChampionship;
using Application.Contract;

namespace Test.Integration.Application
{
	public class TestUpdateSquadApplication : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		public TestUpdateSquadApplication(SharedDatabaseFixture fixture, ITestOutputHelper output)
		{
			(_output, Fixture) = (output, fixture);

			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<SquadEditDTOProfile>();
			});
			_mapper = config.CreateMapper();
		}
		[Fact]
		public void TestValidSquad()
		{
			IServiceResponse result;
			PLayerSquadPostDTO[] squad = PlayerSquadPostDTOExample.Valids;

			Fixture.RunInTransaction(context =>
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.UpdateSquad(squad).Result;
				Assert.True(result.ValidationResult.IsValid);
			});
		}
		[Fact]
		public void TestInvalidSquad()
		{
			IServiceResponse result;
			PLayerSquadPostDTO[] squad = PlayerSquadPostDTOExample.Invalids;

			Fixture.RunInTransaction(context =>
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.UpdateSquad(squad).Result;
				Assert.False(result.ValidationResult.IsValid);
				Assert.Equal(4, result.ValidationResult.Errors.Count);
			});
		}
	}
}
