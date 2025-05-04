using System.Linq;
using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.Adapter;
using Application.DTO;
using Xunit;
using Xunit.Abstractions;
using Infrastructure.Data;
using Application.Services.MatchService;

namespace Test.Integration.Application
{
	public class TestScheduleMatch : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		private readonly IMapper _mapper;
		public SharedDatabaseFixture Fixture { get; }
		public TestScheduleMatch(SharedDatabaseFixture fixture, ITestOutputHelper output)
		{
			(_output, Fixture) = (output, fixture);

			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<MatchEditProfile>();
			});
			_mapper = config.CreateMapper();
		}
		[Fact]
		public void Get_Valid_Championship_Return_List()
		{
			Match[] expected = SeedData.Matches;
			MatchEditsScope result;

			Fixture.RunInTransaction(context =>
			{
				{
					result = new MatchService(_mapper, new UnitOfWork(context, null))
					.GetMatchSchedule("c1").Result;
				}
				Assert.Equal(expected.Select(m => m.Id),
					result.Stages[0].Groups[0].Matchs.Select(m => m.Id));
			});
		}
		[Fact]
		public void Get_Valid_Championship_Return_Empty()
		{
			MatchEditsScope result;

			Fixture.RunInTransaction(context =>
			{
				{
					result = new MatchService(_mapper, new UnitOfWork(context, null))
					.GetMatchSchedule("noexist").Result;
				}
				Assert.Null(result);
			});
		}
		[Fact]
		public void Update_Matches_Return_Ok()
		{
			MatchEditedDTO[] test = new MatchEditedDTO[]
			{
				new MatchEditedDTO
				{
					Id = "m1",
					Name = "alterado"

				},
				new MatchEditedDTO
				{
					Id = "m2",
					Name = "alterado"
				}
			};
			Fixture.RunInTransaction(context =>
			{
				var result = new MatchService(_mapper, new UnitOfWork(context, null))
					.UpdateMatches(test).Result;
				Match[] expected = context.Matchs.ToArray();
				Assert.All(expected, e => Assert.Equal("alterado", e.Name));
			});
		}
	}
}
