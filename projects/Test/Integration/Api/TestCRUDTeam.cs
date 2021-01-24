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
using Xunit;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TeamCRUDTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TeamCRUDTests(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public async Task Get_TeamsList_ReturnTeamList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync("/Team")
				.ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<Team[]>(
				await actualResponse.Content.ReadAsStringAsync());
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team[]>(result);
		}

		[Fact]
		public async Task Get_NonExistingTeam_ReturnEmpty()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync($"/Team/teste")
				.ConfigureAwait(false);
			string RequestResult = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject(RequestResult);
			await actualResponse.Content.ReadAsStringAsync();
			Assert.True(string.IsNullOrEmpty(RequestResult));
		}
		[Fact]
		public async Task Get_Teams_ReturnTeam()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync($"/Team/test")
				.ConfigureAwait(false);

			string requestResult = await actualResponse.Content.ReadAsStringAsync();
			Team teamResult = JsonConvert.DeserializeObject<Team>(requestResult);
			actualResponse.EnsureSuccessStatusCode();
			Assert.NotNull(teamResult);
		}


		[Fact]
		public async Task Post_ValidTeam_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamCreateDTO team = TeamDTODataExample.TeamFull;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PostAsync("/Team", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Team>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team>(result);
		}
		[Fact]
		public async Task Post_InvalidTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamCreateDTO team = TeamDTODataExample.TeamInválid;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PostAsync("/Team", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}


		[Fact]
		public async Task Put_ValidTeam_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateUrl;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Team", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var teamResult = JsonConvert.DeserializeObject<Team>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.IsType<Team>(teamResult);
			Assert.Equal(team.LogoUrl, teamResult.LogoUrl);
			Assert.Equal("test", teamResult.Name);
		}
		[Fact]
		public async Task Put_InvalidTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateNameOnly;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Team", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Put_NonExistinTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateNameOnly;
			team.Id = "non exist";
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Team", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Delete_NonExistinTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.DeleteAsync("/Team/testeinvalid")
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Delete_ValidTeam_ReturnOK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.DeleteAsync("/Team/remove")
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Team>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team>(result);
		}
	}
}
