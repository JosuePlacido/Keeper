using Xunit;
using Xunit.Abstractions;
using AutoMapper;
using AutoMapper.Internal;
using Application.AutoMapper;
using Domain.Models;
using Application.DTO;
using Newtonsoft.Json;
using Test.DataExamples;
using System.Linq;
using Application.Services;
using Infrastructure.Repository;
using Domain.Enum;

namespace Test.Integration.Application
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
			MatchEditsScope result = null;
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
			Assert.True(result.Stages.
				SelectMany(dto => dto.Groups.SelectMany(grp => grp.Matchs)).Count() == 6);
		}
	}
}
