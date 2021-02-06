using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Services.EditChampionship;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class GetSquadsRequests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public GetSquadsRequests(CustomWebApplicationFactoryFixture fixture) =>
			this._fixture = fixture;

		[Fact]
		public void Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();


			HttpResponseMessage actualResponse = client
				.GetAsync("/Championship/Squads/test").Result;

			string json = actualResponse.Content.ReadAsStringAsync().Result;

			var result = JsonConvert.DeserializeObject<SquadEditDTO[]>(json);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<SquadEditDTO[]>(result);
		}
	}
}
