using System.Net.Http;
using Api;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Application.Services.CRUDPlayer;

namespace Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestPaginationPlayerRequest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestPaginationPlayerRequest(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public void Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("/Player/Availables?terms=test").Result;

			var result = JsonConvert.DeserializeObject<PlayerAvailablePaginationDTO>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(1, result.Page);
			Assert.Equal(10, result.Take);
			Assert.Equal("test", result.Terms);
			Assert.Null(result.ExcludeFromChampionship);
			Assert.IsType<PlayerSubscribe[]>(result.Players);
		}
	}
}
