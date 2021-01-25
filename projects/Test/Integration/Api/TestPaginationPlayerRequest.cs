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
