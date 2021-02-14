
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Infrastructure.Repository;
using Keeper.Infrastructure.DAO;
using System.Collections.Generic;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Keeper.Infrastructure.CrossCutting.Adapter;
using Newtonsoft.Json;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Test.Integration.Application
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
			using (var context = Fixture.CreateContext())
			{
				expected = context.Championships.AsNoTracking()
					.Where(c => c.Id == "c1").FirstOrDefault();
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.Rank("c1").Result;
			}
			Assert.NotNull(result);
			Assert.Equal(expected.Name, result.Name);
		}
		[Fact]
		public void Get_Rank_No_Existing_Championship()
		{
			RankDTO result;
			using (var context = Fixture.CreateContext())
			{
				result = new EditChampionshipService(_mapper, new UnitOfWork(context, null))
					.Rank("no exist").Result;
			}
			Assert.Null(result);
		}
		[Fact]
		public void Update_Statistic_Return_OK()
		{
			IServiceResponse result;
			RankPost statistic = new RankPost
			{
				Id = "s1",
				Games = 5
			};
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOChampionship(context);
				var service = new EditChampionshipService(null, new UnitOfWork(context, null));
				result = service.UpdateStatistics(
					new RankPost[] { statistic }
				).Result;
			}
			List<string> newNames = new List<string>();
			Assert.NotNull(result);
			Assert.True(result.ValidationResult.IsValid);
		}
		[Fact]
		public void Update_Statistic_Return_Errors()
		{
			IServiceResponse result;
			using (var context = Fixture.CreateContext())
			{
				var dao = new DAOChampionship(context);
				var service = new EditChampionshipService(null, new UnitOfWork(context, null));
				result = service.UpdateStatistics(null).Result;
			}
			Assert.Null(result.Value);
			Assert.False(result.ValidationResult.IsValid);
		}
	}
}
