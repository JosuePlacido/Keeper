using System.Net;
using System.Net.Http;
using System.Text;
using Keeper.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestRankRequests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestRankRequests(CustomWebApplicationFactoryFixture fixture)
			=> this._fixture = fixture;

		[Fact]
		public void Get_Rank_ReturnOK()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("/Championship/Ranks/test")
				.Result;

			var result = JsonConvert.DeserializeObject<RankDTO>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<RankDTO>(result);
		}

		[Fact]
		public void Get_NonExistingTeam_ReturnEmpty()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("/Championship/Ranks/invalid")
				.Result;

			var result = JsonConvert.DeserializeObject<RankDTO>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.NoContent, actualResponse.StatusCode);
			Assert.Null(result);
		}
		[Fact]
		public void Post_Existing_Statistics()
		{
			RankPost[] statistic = new RankPost[]
			{
				new RankPost{
					Id = "s1",
					Games = 5
				},
				new RankPost{
					Id = "s2",
					Games = 5
				},
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(statistic),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Ranks", content)
				.Result;
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
		}
		[Fact]
		public void Post_No_Existing_Statistics()
		{
			RankPost[] statistic = new RankPost[]
			{
				new RankPost{
					Id = "s1",
					Games = 5
				},
				new RankPost{
					Games = 5
				},
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(statistic),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Ranks", content)
				.Result;

			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(
				actualResponse.Content.ReadAsStringAsync().Result);
			Assert.Equal(HttpStatusCode.BadRequest, actualResponse.StatusCode);
			Assert.NotEmpty(result.Errors);
		}
	}
}
