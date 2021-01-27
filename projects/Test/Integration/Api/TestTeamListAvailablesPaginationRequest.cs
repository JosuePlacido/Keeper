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
using System.Linq;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TestTeamListAvailablePaginationRequest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestTeamListAvailablePaginationRequest(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public void Get_TeamsList_ReturnTeamList()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("Team/Availables?terms=sao").Result;

			var result = JsonConvert.DeserializeObject<TeamPaginationDTO>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.Equal(1, result.Page);
			Assert.Equal(30, result.Take);
			Assert.Equal("sao", result.Terms);
			Assert.True(string.IsNullOrEmpty(result.NotInChampionship));
			Assert.IsType<Team[]>(result.Teams);
		}
	}
}
