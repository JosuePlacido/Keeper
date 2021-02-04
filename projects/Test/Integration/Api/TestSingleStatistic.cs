using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Interface;
using Keeper.Application.Models;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Test.DataExamples;
using Xunit;
using Test.Utils;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TestSingleStatistic : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestSingleStatistic(CustomWebApplicationFactoryFixture fixture)
			=> this._fixture = fixture;

		[Fact]
		public void Get_Statistic_Of_Players()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.GetAsync("Championship/Players/c1")
				.Result;
			var result = JsonConvert.DeserializeObject<PlayerStatisticDTO[]>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<PlayerStatisticDTO[]>(result);
		}

		[Fact]
		public void Get_Statistic_Of_Teams()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.GetAsync($"Championship/Teams/c1")
				.Result;
			var result = JsonConvert.DeserializeObject<TeamStatisticDTO[]>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<TeamStatisticDTO[]>(result);
		}
	}
}
