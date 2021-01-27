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
using Keeper.Domain.Enum;
using System.Linq;
using Test.Utils;

namespace Keeper.Test.Integration.Api
{
	[Collection("WebApi Collection")]
	public class TestUpdateSquad : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestUpdateSquad(CustomWebApplicationFactoryFixture fixture) => this._fixture = fixture;

		[Fact]
		public void Post_EmptySquad_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(new PLayerSquadPostDTO[0]),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Squad", httpContent).Result;
			actualResponse.EnsureSuccessStatusCode();
			Assert.True(actualResponse.IsSuccessStatusCode);
		}

		[Fact]
		public void Post_ValidSquad_ReturnOk()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(new PLayerSquadPostDTO[]{
					new PLayerSquadPostDTO("test","test","test","test",Status.FreeAgent)
				}),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Squad", httpContent).Result;
			actualResponse.EnsureSuccessStatusCode();
			Assert.True(actualResponse.IsSuccessStatusCode);
			string json = actualResponse.Content.ReadAsStringAsync().Result;
			PlayerSubscribe[] result = JsonConvert.DeserializeObject<PlayerSubscribe[]>(json,
			new JsonSerializerSettings
			{
				ContractResolver = new PrivateResolver()
			});
			Assert.Equal(Status.FreeAgent, result[0].Status);
		}
		[Fact]
		public void Post_InvalidSquads_ReturnBadRequest()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent httpContent = new StringContent(
				JsonConvert.SerializeObject(PlayerSquadPostDTOExample.Invalids),
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Squad", httpContent).Result;
			string request = actualResponse.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ValidationProblemDetails>(request);
			Assert.False(actualResponse.IsSuccessStatusCode);
			Assert.Equal(result.Status, 400);
			Assert.NotEmpty(result.Errors);
		}
	}
}
