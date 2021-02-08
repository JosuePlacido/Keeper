using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Keeper.Api;
using Keeper.Application.Contract;
using Keeper.Application.Services.CreateChampionship;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Test.DataExamples;
using Xunit;
using Test.Utils;
using Keeper.Application.Services.MatchService;

namespace Keeper.Test.EndToEnd
{
	[Collection("WebApi Collection")]
	public class TestScheduleMatchRequest : IClassFixture<WebApplicationFactory<Startup>>
	{
		private readonly CustomWebApplicationFactoryFixture _fixture;
		public TestScheduleMatchRequest(CustomWebApplicationFactoryFixture fixture)
			=> this._fixture = fixture;

		[Fact]
		public void Get_Matches_Return_OK()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.GetAsync("Match/Schedule/c1")
				.Result;
			var result = JsonConvert.DeserializeObject<MatchEditsScope>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<MatchEditsScope>(result);
		}

		[Fact]
		public void Get_Matches_Return_No_Content()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.GetAsync($"Match/Schedule/noexist")
				.Result;
			var result = JsonConvert.DeserializeObject<MatchEditsScope>(
				actualResponse.Content.ReadAsStringAsync().Result);
			Assert.Equal(HttpStatusCode.NoContent, actualResponse.StatusCode);
			Assert.Null(result);
		}
		[Fact]
		public void Check_Matches_Return_Ok()
		{
			MatchEditsScope test = new MatchEditsScope
			{
				Stages = new MatchStageEdit[]{
						new MatchStageEdit{
							Id = "1",
							DuplicateTurn = false,
							Groups = new MatchGroupEdit[]{
								new MatchGroupEdit{
									StageId = "1",
									Matchs = new MatchItemDTO []{
										new MatchItemDTO{
											Id = "1",
											Round = 1,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "2",
											Round = 1,
											HomeId = "AAA",
											Home = new MatchEditTeam{
												Team = "AAA"
											},
											AwayId = "BBB",
											Away = new MatchEditTeam{
												Team = "BBB"
											},
										},
										new MatchItemDTO{
											Id = "3",
											Round = 2,
											HomeId = "CCC",
											Home = new MatchEditTeam{
												Team = "CCC"
											},
											AwayId = "AAA",
											Away = new MatchEditTeam{
												Team = "AAA"
											},
										},
										new MatchItemDTO{
											Id = "4",
											Round = 2,
											HomeId = "BBB",
											Home = new MatchEditTeam{
												Team = "BBB"
											},
											AwayId = "CCC",
											Away = new MatchEditTeam{
												Team = "CCC"
											},
										}

									}
								}
							}
						}
					}
			};

			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(test),
				Encoding.UTF8, "application/json"); ;
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.PostAsync($"Match/Check", content)
				.Result;
			var result = JsonConvert.DeserializeObject<MatchEditsScope>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<MatchEditsScope>(result);
		}
		[Fact]
		public void Check_Matches_NULL_Return_NoContent()
		{
			HttpContent content = new StringContent(
				JsonConvert.SerializeObject(new { }),
				Encoding.UTF8, "application/json"); ;
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();
			HttpResponseMessage actualResponse = client
				.PostAsync($"Match/Check", content)
				.Result;
			var result = JsonConvert.DeserializeObject<MatchEditsScope>(
				actualResponse.Content.ReadAsStringAsync().Result);
			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.Null(result.Errors);
			Assert.Null(result.Id);
		}
		[Fact]
		public void Post_Matches_Return_Ok()
		{
			HttpClient client = this._fixture
				.CustomWebApplicationFactory
				.CreateClient();

			MatchEditedDTO[] test = new MatchEditedDTO[]
			{
				new MatchEditedDTO
				{
					Id = "m1",
					Name = "alterado"

				},
				new MatchEditedDTO
				{
					Id = "m2",
					Name = "alterado"
				}
			};

			string jsonPost = JsonConvert.SerializeObject(test);

			HttpContent httpContent = new StringContent(
				jsonPost,
				Encoding.UTF8, "application/json");

			HttpResponseMessage actualResponse = client
				.PostAsync($"Match/Schedule", httpContent)
				.Result;

			string json = actualResponse.Content.ReadAsStringAsync().Result;

			var result = JsonConvert.DeserializeObject<Match[]>(json);

			actualResponse.EnsureSuccessStatusCode();
			Assert.Equal(HttpStatusCode.OK, actualResponse.StatusCode);
			Assert.IsType<Match[]>(result);
		}
	}
}
