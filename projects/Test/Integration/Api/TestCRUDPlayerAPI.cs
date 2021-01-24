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
	public class PlayerCRUDTests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public PlayerCRUDTests(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public async Task Get_PlayersList_ReturnPlayerList()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync("/Player")
				.ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<PlayerPaginationDTO>(
				await actualResponse.Content.ReadAsStringAsync());
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player[]>(result.Players);
		}

		[Fact]
		public async Task Get_NonExistingPlayer_ReturnEmpty()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync($"/Player/teste")
				.ConfigureAwait(false);
			string RequestResult = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject(RequestResult);
			await actualResponse.Content.ReadAsStringAsync();
			Assert.True(string.IsNullOrEmpty(RequestResult));
		}
		[Fact]
		public async Task Get_Players_ReturnPlayer()
		{

			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync($"/Player/test")
				.ConfigureAwait(false);

			string requestResult = await actualResponse.Content.ReadAsStringAsync();
			Player playerResult = JsonConvert.DeserializeObject<Player>(requestResult);
			actualResponse.EnsureSuccessStatusCode();
			Assert.NotNull(playerResult);
		}


		[Fact]
		public async Task Post_ValidPlayer_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerCreateDTO player = PlayerDTODataExample.PlayerFull;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PostAsync("/Player", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Player>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player>(result);
		}
		[Fact]
		public async Task Post_InvalidPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerCreateDTO player = PlayerDTODataExample.PlayerInválid;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PostAsync("/Player", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}


		[Fact]
		public async Task Put_ValidPlayer_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNickname;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Player", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
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
		public async Task Put_InvalidPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNameOnly;
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Player", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Put_NonExistinPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			PlayerUpdateDTO player = PlayerDTODataExample.PlayerUpdateNameOnly;
			player.Id = "non exist";
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(player),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = await client
				.PutAsync("/Player", httpContent)
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.True(!actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Delete_NonExistinPlayer_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.DeleteAsync("/Player/testeinvalid")
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.True(result.Errors.Count > 0);
		}
		[Fact]
		public async Task Delete_ValidPlayer_ReturnOK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.DeleteAsync("/Player/remove")
				.ConfigureAwait(false);
			string request = await actualResponse.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<Player>(request);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Player>(result);
		}
	}
}
