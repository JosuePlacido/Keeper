
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Keeper.Domain.Enum;
using Keeper.Test.DataExamples;
using Newtonsoft.Json;
using Keeper.Infrastructure.CrossCutting.Adapter;

namespace Keeper.Test.UnitTest.Application.Validation
{
	public class CreateChampionshipValidationTest
	{
		private readonly ITestOutputHelper _output;
		public CreateChampionshipValidationTest(ITestOutputHelper output)
			=> _output = output;

		[Fact]
		public void TestValidChampionship()
		{
			Championship championship = Championship.Factory("test", "test", "test",
				stages: new Stage[]
				{
					Stage.Factory(
						"s1",
						"test",
						duplicateTurn:true,
						criterias: "0,1,2,3,4,5,6,7",
						name: "Final",
						order: 0,
						typeStage: TypeStage.Knockout,
						regulation: Classifieds.Configured,
						groups: new Group[]
							{
								Group.Factory("g1","Final", "s1",
									statistics:new Statistic[0],
									vacancys: new Vacancy[] {
										Vacancy.Factory("v1","Campeão da Libertadores 1993",
											"g1", Classifieds.Configured),
										Vacancy.Factory("v2","Campeão da Supercopa 1993",
											"g1", Classifieds.Configured),
									},
									matchs: new Match[]
									{
										Match.Factory("m1", round: 1,groupId:"g1", name: "Final"
											, vacancyHomeId: "v1", vacancyAwayId: "v2")
									}
								)
							}
						)
				});
			var validation = new CreateChampionshipValidation()
				.ValidateScope()
				.ValidateSecond().Validate(championship);
			Assert.True(validation.IsValid);
		}
		[Fact]
		public void TestInvalidChampionship()
		{
			Championship championship = Championship.Factory("test", "test", "edition", SeedData.Categorys.First(), Status.Created);
			var validation = new CreateChampionshipValidation()
				.ValidateScope()
				.ValidateSecond().Validate(championship);
			Assert.False(validation.IsValid);
		}
		[Fact]
		public void TestMapping()
		{
			var championship = ChampionshipCreateDTODataExamples.SemiFinal;
			MapperConfiguration config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ChampionshipDTOToDomainProfile>();
			});
			IMapper mapper = config.CreateMapper();
			var result = mapper.Map<Championship>(championship);
			Assert.Equal(2, championship.Stages[1].Groups[0].Vacancys.Count());
		}
	}
}
