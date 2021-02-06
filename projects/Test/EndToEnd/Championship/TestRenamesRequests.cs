using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Services.EditChampionship;
using Keeper.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestRenamesRequests : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestRenamesRequests(CustomWebApplicationFactoryFixture fixture) =>
			this._fixture = fixture;

		[Fact]
		public async Task Get_Structure_For_Rename()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpResponseMessage actualResponse = await client
				.GetAsync("/Championship/Names/test")
				.ConfigureAwait(false);

			var result = JsonConvert.DeserializeObject<ObjectRenameDTO>(
				await actualResponse.Content.ReadAsStringAsync());
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<ObjectRenameDTO>(result);
		}
		[Fact]
		public void RenameChampionshipRequests()
		{
			ObjectRenameDTO test = new ObjectRenameDTO
			{
				Id = "test",
				Name = "test edit"
			};
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(test),
				Encoding.UTF8, "application/json");
			HttpResponseMessage actualResponse = client
				.PostAsync("/Championship/Names", content).Result;

			var result = JsonConvert.DeserializeObject<Championship>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Championship>(result);
		}
	}
}
