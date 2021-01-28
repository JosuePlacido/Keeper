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
using Test.Utils;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TeamCRUDTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TeamCRUDTests(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public void Get_TeamsList_ReturnTeamList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("/Team")
				.Result;

			var result = JsonConvert.DeserializeObject<Team[]>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team[]>(result);
		}

		[Fact]
		public void Get_NonExistingTeam_ReturnEmpty()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync($"/Team/teste")
				.Result;
			string RequestResult = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject(RequestResult);
			Assert.True(string.IsNullOrEmpty(RequestResult));
		}
		[Fact]
		public void Get_Teams_ReturnTeam()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync($"/Team/test")
				.Result;

			string requestResult = actualResponse.Content.ReadAsStringAsync().Result;
			Team teamResult = JsonConvert.DeserializeObject<Team>(requestResult);
			actualResponse.EnsureSuccessStatusCode();
			Assert.NotNull(teamResult);
		}


		[Fact]
		public void Post_ValidTeam_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamCreateDTO team = TeamDTODataExample.TeamFull;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Team", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<Team>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team>(result);
		}
		[Fact]
		public void Post_InvalidTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamCreateDTO team = TeamDTODataExample.TeamInválid;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Team", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}


		[Fact]
		public void Put_ValidTeam_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateUrl;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Team", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var teamResult = JsonConvert.DeserializeObject<Team>(request,
				new JsonSerializerSettings()
				{
					ContractResolver = new PrivateResolver()
				});
			actualResponse.EnsureSuccessStatusCode();
			Assert.IsType<Team>(teamResult);
			Assert.Equal(team.LogoUrl, teamResult.LogoUrl);
			Assert.Equal("test", teamResult.Name);
		}
		[Fact]
		public void Put_InvalidTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateNameOnly;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Team", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Put_NonExistinTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			TeamUpdateDTO team = TeamDTODataExample.TeamUpdateNameOnly;
			team.Id = "non exist";
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(team),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Team", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Delete_NonExistinTeam_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.DeleteAsync("/Team/testeinvalid")
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Delete_ValidTeam_ReturnOK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.DeleteAsync("/Team/remove")
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<Team>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Team>(result);
		}
	}
}
