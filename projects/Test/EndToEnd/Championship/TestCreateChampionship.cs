using System.Net;
using System.Net.Http;
using System.Text;
using Keeper.Api;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Keeper.Test.DataExamples;
using Keeper.Application.Services.CreateChampionship;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestCreateChampionship : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestCreateChampionship(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;
		[Fact]
		public void Post_ValidChampionship_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			ChampionshipCreateDTO championship = ChampionshipCreateDTODataExamples.SemiFinal;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(championship),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<MatchEditsScope>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<MatchEditsScope>(result);
		}
		[Fact]
		public void Post_InvalidTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			ChampionshipCreateDTO championship = ChampionshipCreateDTODataExamples.Invalid;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(championship),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
	}
}