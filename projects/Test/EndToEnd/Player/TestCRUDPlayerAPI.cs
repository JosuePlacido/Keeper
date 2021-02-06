using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Contract;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Test.DataExamples;
using Test.Utils;
using Xunit;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class PlayerCRUDTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public PlayerCRUDTests(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public void Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync("/Player").Result;

			var result = JsonConvert.DeserializeObject<Player[]>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player[]>(result);
		}

		[Fact]
		public void Get_NonExistingPlayer_ReturnEmpty()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync($"/Player/teste").Result;
			string RequestResult = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject(RequestResult);
			Assert.True(string.IsNullOrEmpty(RequestResult));
		}
		[Fact]
		public void Get_Players_ReturnPlayer()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.GetAsync($"/Player/test")
				.Result;

			string requestResult = actualResponse.Content.ReadAsStringAsync().Result;
			Player playerResult = JsonConvert.DeserializeObject<Player>(requestResult);
			actualResponse.EnsureSuccessStatusCode();
			Assert.NotNull(playerResult);
		}


		[Fact]
		public void Post_ValidPlayer_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerCreateDTO player = PlayerDTODataExample.PlayerFull;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Player", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<Player>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player>(result);
		}
		[Fact]
		public void Post_InvalidPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerCreateDTO player = PlayerDTODataExample.PlayerInválid;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Player", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}


		[Fact]
		public void Put_ValidPlayer_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNickname;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Player", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var playerResult = JsonConvert.DeserializeObject<Player>(request, new JsonSerializerSettings()
			{
				ContractResolver = new PrivateResolver()
			});
			actualResponse.EnsureSuccessStatusCode();
			Assert.IsType<Player>(playerResult);
			Assert.Equal(player.Nickname, playerResult.Nickname);
			Assert.Equal("test", playerResult.Name);
		}
		[Fact]
		public void Put_InvalidPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNameOnly;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Player", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Put_NonExistinPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNameOnly;
			player.Id = "non exist";
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PutAsync("/Player", httpContent)
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Delete_NonExistinPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.DeleteAsync("/Player/testeinvalid")
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(400, result.Status);
			Assert.NotEmpty(result.Errors);
		}
		[Fact]
		public void Delete_ValidPlayer_ReturnOK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = client
				.DeleteAsync("/Player/remove")
				.Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<Player>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player>(result);
		}
	}
}
