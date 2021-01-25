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
		public void TestTiebreakCriterionOrdering()
		{
			var teste = criterionList[0].Order(example.Select((e, i) =>
			{
				e.Reorder(1);
				return e;
			}).ToArray());
			for (int x = 1; x < criterionList.Length; x++)
			{
				teste = criterionList[x].Order(teste);
			}
			Statistic[] result = teste.ToArray();
			int c = 0;
			int position = 0;
			int range = 1;
			bool allDraw = true;
			for (int x = 0; x < result.Length; x++)
			{
				c = 0;
				allDraw = x > 0;
				while (allDraw && c < criterionList.Length)
				{
					allDraw = criterionList[c].Criterion.Invoke(result[x]) ==
						criterionList[c].Criterion.Invoke(result[x - 1]);
					c++;
				}
				if (allDraw)
				{
					range++;
				}
				else
				{
					position += range;
					range = 1;
				}
				result[x].Reorder(position);
			}
			PrintResult(teste.ToArray());
			Assert.Equal(example.OrderBy(s => s.Position), teste.ToArray());
		}
		private TiebreackCriterion[] criterionList = TiebreackCriterion.GetAll<TiebreackCriterion>().Take(7).ToArray();
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
