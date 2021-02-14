using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Services.RegisterResult;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Test.Utils;
using Xunit;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestRegisterResultRequests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestRegisterResultRequests(CustomWebApplicationFactoryFixture fixture)
			=> this._fixture = fixture;

		[Fact]
		public void Get_Match_Return_OK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.GetAsync("Match/m1")
				.Result;

			string json = actualResponse.Content.ReadAsStringAsync().Result;

			var result = JsonConvert.DeserializeObject<Match>(json,
				new JsonSerializerSettings()
				{
					ContractResolver = new PrivateResolver(),
					ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
				});
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Match>(result);
		}
		[Fact]
		public void Post_Match_Result_Reutnr_Ok()
		{
			MatchResultDTO test = new MatchResultDTO
			{
				Id = "m2",
				GoalsHome = 0,
				GoalsAway = 0,
				GoalsPenaltyAway = 4,
				GoalsPenaltyHome = 0,
				Events = new EventGameDTO[]{
					new EventGameDTO{
						Description = "Lesão visitante",
						PlayerId = "ps1",
						IsHomeEvent = false,
						Type = TypeEvent.Injury,
						MatchId = "m2"
					},
					new EventGameDTO{
						Description = "MVP visitante",
						PlayerId = "ps1",
						IsHomeEvent = false,
						Type = TypeEvent.MVP,
						MatchId = "m2"
					},
				}
			};

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(test),
				Encoding.UTF8, "application/json");

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.PostAsync("Match", content)
				.Result;
			var result = JsonConvert.DeserializeObject<Match>(
				actualResponse.Content.ReadAsStringAsync().Result,
				new JsonSerializerSettings()
				{
					ContractResolver = new PrivateResolver(),
					ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
				});
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Match>(result);
		}
	}
}
