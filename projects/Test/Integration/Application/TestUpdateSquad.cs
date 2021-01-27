using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Keeper.Application.DTO;
using Keeper.Infrastructure.Repository;
using Keeper.Test;
using Test.DataExamples;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.DAO;
using Infrastructure.Data;
using Keeper.Domain.Enum;
using Keeper.Application.Interface;

namespace Keeper.Test.Integration.Application
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
			IServiceResult result;
			PLayerSquadPostDTO[] squad = PlayerSquadPostDTOExample.Valids;
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new ChampionshipService(_mapper, new UnitOfWork(context), repo,
					new DAOPlayerSubscribe(context))
				.UpdateSquad(squad).Result;
			}
			Assert.True(result.ValidationResult.IsValid);
		}
		[Fact]
		public void TestInvalidSquad()
		{
			IServiceResult result;
			PLayerSquadPostDTO[] squad = PlayerSquadPostDTOExample.Invalids;
			using (var context = Fixture.CreateContext())
			{
				ChampionshipRepository repo = new ChampionshipRepository(context);
				result = new ChampionshipService(_mapper, new UnitOfWork(context), repo,
					new DAOPlayerSubscribe(context))
								.UpdateSquad(squad).Result;
			}
			Assert.False(result.ValidationResult.IsValid);
			Assert.Equal(4, result.ValidationResult.Errors.Count);
		}
	}
}
