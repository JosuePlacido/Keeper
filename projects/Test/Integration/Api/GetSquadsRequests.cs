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
	public class GetSquadsRequests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public GetSquadsRequests(CustomWebApplicationFactoryFixture fixture) =>
			this._fixture = fixture;

		[Fact]
		public async Task Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync("/Championship/Squads/test")
				.ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<SquadEditDTO[]>(
				await actualResponse.Content.ReadAsStringAsync());
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<SquadEditDTO[]>(result);
		}
	}
}
