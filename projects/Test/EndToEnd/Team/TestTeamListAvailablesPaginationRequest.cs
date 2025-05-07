using System.Net;
using System.Net.Http;
using Api;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Application.DTO;

namespace Test.EndToEnd
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

			var result = JsonConvert.DeserializeObject<PaginationDTO<Team>>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.Equal(1, result.Page);
			Assert.Equal(30, result.Take);
			Assert.IsType<Team[]>(result.Items);
		}
	}
}
