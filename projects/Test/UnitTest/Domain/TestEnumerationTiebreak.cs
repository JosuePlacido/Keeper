using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Keeper.Domain.Models;
using Keeper.Domain.Enum;
using Keeper.Test.Domain;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Test.UnitTest.Domain
{
	public class TestEnumerationTiebreak
	{
		private readonly ITestOutputHelper _output;
		public TestEnumerationTiebreak(ITestOutputHelper output)
			=> _output = output;

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
		}
		private void PrintResult(Statistic[] ranking)
		{
			_output.WriteLine("---- RANK ----");
			foreach (var item in ranking)
			{
				_output.WriteLine($"{item.Position} {item.TeamSubscribeId} {item.Points} "
				+ $"{item.Won} {item.Yellows}");
			};
		}

		[Fact]
		public void TestSimplesTiebreakCriterionOrdering()
		{
			var testCase = example.Select((e, i) => e.Reorder(1)).ToArray();
			var teste = criterionList[0].Order(testCase);
			for (int x = 1; x < criterionList.Length; x++)
			{
				teste = criterionList[x].Order(teste);
			}
			PrintResult(teste.ToArray());
			var positions = teste.Select(t => t.Position).ToArray();
			Assert.Equal(example.OrderBy(s => s.Position), teste.ToArray());
		}
		[Fact]
		public void TestRandomCriterion()
		{
			var preRandom = example.Select(t => t.Position).ToArray();
			var teste = TiebreackCriterion.Random.Order(example.OrderBy(s => s.Position).ToArray());
			//PrintResult(teste.ToArray());
			var positions = teste.Select(t => t.Position).ToArray();
			Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, positions);
		}
		[Fact]
		public void TestPenaltiesCriterion()
		{
			var preRandom = example.Select(t => t.Position).ToArray();
			var teste = TiebreackCriterion.Random.Order(
				example.Where(s => s.Position == 3).ToArray(),
				((id, ids) => new Match[]{
				Match.Factory("m1","final","group",1,Status.Finish,
					aggregateGame:true,penalty:true, finalGame: true,
					homeId:"4",goalsHome:0,aggregateGoalsHome:0,goalsPenaltyHome:3,
					awayId:"3",goalsAway:0,aggregateGoalsAway:0,goalsPenaltyAway:1)
			}));
			var positions = teste.Select(t => t.Position).ToArray();
			Assert.Equal(3, teste[0].Position);
			Assert.Equal(4, teste[1].Position);
		}
		[Fact]
		public void TestGoalsAwayCriterion()
		{
			var preRandom = example.Select(t => t.Position).ToArray();
			var teste = TiebreackCriterion.Random.Order(
				example.Where(s => s.Position == 3).ToArray(),
				((id, ids) => new Match[]{
				Match.Factory("m1","final ida","group",1,Status.Finish,
					aggregateGame:true,
					homeId:"3",goalsHome:2,aggregateGoalsHome:4,
					awayId:"4",goalsAway:4,aggregateGoalsAway:4),
				Match.Factory("m2","final volta","group",1,Status.Finish,
					aggregateGame:true,penalty:true, finalGame: true,
					homeId:"4",goalsHome:0,aggregateGoalsHome:4,
					awayId:"3",goalsAway:2,aggregateGoalsAway:4)
			}));
			var positions = teste.Select(t => t.Position).ToArray();
			Assert.Equal(3, teste[0].Position);
			Assert.Equal(4, teste[1].Position);
		}
		[Fact]
		public void TestDirectMatchCriterion()
		{
			var preRandom = example.Select(t => t.Position).ToArray();
			var teste = TiebreackCriterion.Random.Order(
				example.Where(s => s.Position == 3).ToArray(),
				((id, ids) => new Match[]{
				Match.Factory("m1","jogo 1","group",1,Status.Finish,
					homeId:"3",goalsHome:2,
					awayId:"4",goalsAway:4),
				Match.Factory("m2","jogo 2","group",1,Status.Finish,
					homeId:"4",goalsHome:2,
					awayId:"3",goalsAway:2)
			}));
			var positions = teste.Select(t => t.Position).ToArray();
			Assert.Equal(3, teste[0].Position);
			Assert.Equal(4, teste[1].Position);
		}
		private TiebreackCriterion[] criterionList = TiebreackCriterion
			.GetAll<TiebreackCriterion>().Take(7).ToArray();
		private Statistic[] example = new Statistic[]{
				Statistic.Factory("8","HHH","group",points: 6,won:0,goalsScores:6,
					goalsAgainst:6,goalsDifference:0,yellows: 20,reds:0,position:8),
				Statistic.Factory("7","GGG","group",points: 10,won:2,goalsScores:9,
					goalsAgainst:4,goalsDifference:5,yellows: 13,reds:0,position:5),
				Statistic.Factory("2","BBB","group",points: 12,won:4,goalsScores:7,
					goalsAgainst:11,goalsDifference:-4,yellows: 11,reds:0,position:2),
				Statistic.Factory("4","DDD","group",points: 10,won:3,goalsScores:9,
					goalsAgainst:10,goalsDifference:-1,yellows: 8,reds:1,position:3),
				Statistic.Factory("6","FFF","group",points: 6,won:1,goalsScores:7,
					goalsAgainst:12,goalsDifference:-5,yellows: 10,reds:3,position:7),
				Statistic.Factory("5","EEE","group",points: 6,won:1,goalsScores:7,
					goalsAgainst:12,goalsDifference:-5,yellows: 6,reds:3,position:6),
				Statistic.Factory("1","AAA","group",points: 19,won:6,goalsScores:17,
					goalsAgainst:7,goalsDifference:10,yellows: 5,reds:2,position:1),
				Statistic.Factory("3","CCC","group",points: 10,won:3,goalsScores:9,
					goalsAgainst:10,goalsDifference:-1,yellows: 8,reds:1,position:3),
			};
	}
}
