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

namespace Test.Application.Mapping
{
	public class ChampionshipScopeCreate : IClassFixture<SharedDatabaseFixture>
	{
		private readonly ITestOutputHelper _output;
		public SharedDatabaseFixture Fixture { get; }
		public ChampionshipScopeCreate(SharedDatabaseFixture fixture, ITestOutputHelper output)
			=> (_output, Fixture) = (output, fixture);
		/*
				[Theory]
				[ClassData(typeof(CreateChampionshipSetup))]
				public void TestMappingForChampionshipEntity(ChampionshipCreateDTOModelTest test)
				{
					var config = new MapperConfiguration(cfg =>
					{
						cfg.AddProfile<ChampionshipDTOToDomainProfile>();
					});
					var mapper = config.CreateMapper();
					var championship = mapper.Map<Championship>(test.Enter);
					for (int s = 0; s < test.Expected.Stages.Count(); s++)
					{
						var stage = test.Expected.Stages.ElementAt(s);
						for (int x = 0; x < stage.Groups.Count(); x++)
						{
							stage.Groups.ElementAt(x).Id = championship.Stages.ElementAt(s).Groups.ElementAt(x).Id;
						}

					}
					for (int x = 0; x < test.Expected.Teams.Count(); x++)
					{
						test.Expected.Teams.ElementAt(x).Id = championship.Teams.ElementAt(x).Id;
						if (test.Expected.Teams.ElementAt(x).Players != null)
						{
							var length = test.Expected.Teams.ElementAt(x).Players.Count();
							for (int y = 0; y < length; y++)
							{
								test.Expected.Teams.ElementAt(x).Players.ElementAt(y).Id =
									championship.Teams.ElementAt(x).Players.ElementAt(y).Id;
							}
						}
					}
					var result = JsonConvert.SerializeObject(championship, Formatting.Indented, new JsonSerializerSettings
					{
						ReferenceLoopHandling = ReferenceLoopHandling.Serialize
					});
					var expected = JsonConvert.SerializeObject(test.Expected, Formatting.Indented, new JsonSerializerSettings
					{
						ReferenceLoopHandling = ReferenceLoopHandling.Serialize
					});
					config.AssertConfigurationIsValid();
					_output.WriteLine(result);
					_output.WriteLine(expected);
					Assert.Equal(result, expected);
				}*/

		[Fact]
		public void TestAddingReference()
		{
			using (var context = Fixture.CreateContext())
			{
				var config = new MapperConfiguration(cfg =>
				{
					cfg.AddProfile<ChampionshipDTOToDomainProfile>();
				});
				var mapper = config.CreateMapper();
				var repo = new ChampionshipRepository(context);
				var teams = context.Set<Team>().Select(ts => ts.Id).ToArray();
				var players = context.Set<Player>().Select(ts => ts.Id).ToArray();
				var test = ChampionshipCreateDTODataExamples.SemiFinal;
				var championship = new ChampionshipService(mapper, repo).Create(test);
				_output.WriteLine(JsonConvert.SerializeObject(championship, Formatting.Indented, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				}));
				Assert.NotNull(championship);
			}
		}
	}
}