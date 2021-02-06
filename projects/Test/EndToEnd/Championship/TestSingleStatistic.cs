using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Services.EditChampionship;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Keeper.Test.EndToEnd
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
		[Fact]
		public void Post_Statistic_Of_Teams()
		{
			TeamSubscribePost test = new TeamSubscribePost
			{
				Id = "ts1",
				Games = 5
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(new TeamSubscribePost[] { test }),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync($"Championship/Teams", content)
				.Result;
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
		}
		[Fact]
		public void Post_Statistic_Of_Players()
		{
			PlayerSubscribePost test = new PlayerSubscribePost
			{
				Id = "ps1",
				Games = 5
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(new PlayerSubscribePost[] { test }),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync($"Championship/Players", content)
				.Result;
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
		}
		[Fact]
		public void Post_Statistic_Of_Players_Invalid()
		{
			PlayerSubscribePost test = new PlayerSubscribePost
			{
				Games = 5
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(new PlayerSubscribePost[] { test }),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync($"Championship/Players", content)
				.Result;
			var result = JsonConvert.DeserializeObject<ProblemDetails>(
				actualResponse.Content.ReadAsStringAsync().Result);
			Assert.Equal(HttpStatusCode.BadRequest, actualResponse.StatusCode);
		}
		[Fact]
		public void Post_Statistic_Of_Teams_Invalid()
		{
			TeamSubscribePost test = new TeamSubscribePost
			{
				Games = 5
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(new TeamSubscribePost[] { test }),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync($"Championship/Teams", content)
				.Result;
			var result = JsonConvert.DeserializeObject<ProblemDetails>(
				actualResponse.Content.ReadAsStringAsync().Result);
			Assert.Equal(HttpStatusCode.BadRequest, actualResponse.StatusCode);
		}
	}
}
