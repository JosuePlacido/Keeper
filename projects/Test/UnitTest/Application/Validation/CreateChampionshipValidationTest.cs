
using System.Linq;
using Keeper.Application.Services;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Keeper.Domain.Enum;

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
			Championship championship = SeedData.GetChampionship();
			var validation = new CreateChampionshipValidation().Validate(championship);
			Assert.True(validation.IsValid);
		}
		[Fact]
		public void TestInvalidChampionship()
		{
			Championship championship = new Championship("test", "edition", SeedData.Categorys.First(), Status.Created);
			var validation = new CreateChampionshipValidation().Validate(championship);
			Assert.False(validation.IsValid);
		}
	}
}
