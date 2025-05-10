
using System.Linq;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.DAO;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Services.EditChampionship;
using Application.Contract;

namespace Test.Integration.Application
{
	public class TestRankService : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		public TestRankService(SharedDatabaseFixture fixture,
			ITestOutputHelper output)
		{
			(_output, Fixture) = (output, fixture);
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<RankDTOProfile>();
			});
			_mapper = config.CreateMapper();
		}

		[Fact]
		public void Get_Rank_For_Valid_Championship()
		{
			RankDTO result;
			Championship expected;
			Fixture.RunInTransaction(context =>
			{
				expected = context.Championships.AsNoTracking()
					.Where(c => c.Id == "c1").FirstOrDefault();
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.Rank("c1").Result;
				Assert.NotNull(result);
				Assert.Equal(expected.Name, result.Name);
			});
		}
		[Fact]
		public void Get_Rank_No_Existing_Championship()
		{
			RankDTO result;
			Fixture.RunInTransaction(context =>
			{
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.Rank("no exist").Result;
				Assert.Null(result);
			});
		}
		[Fact]
		public void Update_Statistic_Return_OK()
		{
			IServiceResponse result;
			Fixture.RunInTransaction(context =>
				{

					RankPost statistic = new RankPost
					{
						Id = "s1",
						Games = 5
					};
					var dao = new DAOChampionship(context);
					var service = new EditChampionshipService(null, new UnitOfWork(context, null));
					result = service.UpdateStatistics(
							new RankPost[] { statistic }
						).Result;
					List<string> newNames = new List<string>();

					Assert.NotNull(result);
					Assert.True(result.ValidationResult.IsValid);
				});
		}
		[Fact]
		public void Update_Statistic_Return_Errors()
		{
			IServiceResponse result;
			Fixture.RunInTransaction(context =>
			{
				var dao = new DAOChampionship(context);
				var service = new EditChampionshipService(null, new UnitOfWork(context, null));
				result = service.UpdateStatistics(null).Result;

				Assert.Null(result.Value);
				Assert.False(result.ValidationResult.IsValid);
			});
		}
	}
}
