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
using Test.Utils;
using Xunit;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TestPaginationPlayerRequest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestPaginationPlayerRequest(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public async Task Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync("/Player?terms=test")
				.ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<PlayerPaginationDTO>(
				await actualResponse.Content.ReadAsStringAsync());
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.True(result.Page == 1);
			Assert.True(result.Take == 10);
			Assert.True(result.Terms == "test");
			Assert.True(string.IsNullOrEmpty(result.ExcludeFromChampionship));
			Assert.IsType<Player[]>(result.Players);
		}
	}
}
