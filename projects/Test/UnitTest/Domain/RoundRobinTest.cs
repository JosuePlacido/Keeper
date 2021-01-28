using System;
using System.Linq;
using Keeper.Domain.Models;
using Keeper.Test.Domain;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Domain
{
	public class RoundRobinTest
	{
		private readonly ITestOutputHelper _output;
		public RoundRobinTest(ITestOutputHelper output)
			=> _output = output;

		private void Print(object item)
		{
			_output.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
		}
		private string[] NormalizeTeams(MatchShortFormat[] matchList)
		{
			var teams = matchList.Select(j => j.Home).Distinct().ToList();
			teams.AddRange(matchList.Select(j => j.Away).Distinct().ToList());
			return teams.Distinct().ToArray();
		}

		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void TestValidUniqueMatches(Group group, bool hasReturn, bool isMirror)
		{
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match)).ToArray();
			var uniqueMatches = matchList.GroupBy(match => new { match.Home, match.Away }).Count();
			Assert.True(matchList.Count() == uniqueMatches);
		}

		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void TestMatchesSequencesHomeOrAway(Group group, bool hasReturn, bool isMirror)
		{
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match)).ToArray();
			var haveBigSequenceLikeHomeOrAway = false;
			for (int x = 1; x < matchList.Length - 1; x++)
			{
				if ((matchList[x].Home == matchList[x - 1].Home
					&& matchList[x].Home == matchList[x + 1].Home) ||
					(matchList[x].Away == matchList[x - 1].Away
					&& matchList[x].Away == matchList[x + 1].Away))
				{
					haveBigSequenceLikeHomeOrAway = true;
					break;
				}
			}
			Assert.False(haveBigSequenceLikeHomeOrAway);
		}

		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void TestMatchesPerTeam(Group group, bool hasReturn, bool isMirror)
		{
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match)).ToArray();

			var teams = NormalizeTeams(matchList);
			var correctCount = teams.Length - 1;
			if (hasReturn)
			{
				correctCount *= 2;
			}
			var approved = true;
			for (var i = 0; i < teams.Length; i++)
			{
				var matchesFound = matchList.Where(match => match.Home == teams[i] ||
					match.Away == teams[i]).Count();
				if (correctCount != matchesFound)
				{
					approved = false;
					break;
				}
			}
			Assert.True(approved);
		}
		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void BalanceMatchesLikeHomeAndAway(Group group, bool hasReturn, bool isMirror)
		{
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match)).ToArray();
			var teams = NormalizeTeams(matchList);
			var approved = true;
			for (var i = 0; i < teams.Length; i++)
			{
				var homeMatches = matchList.Where(match => match.Home == teams[i]).Count();
				var awayMatches = matchList.Where(match => match.Home == teams[i]).Count();
				if (Math.Abs(homeMatches - awayMatches) > 1)
				{
					approved = false;
					break;
				}
			}
			Assert.True(approved);
		}
		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void RoundNumberGeneratedInReturn(Group group, bool hasReturn, bool isMirror)
		{
			hasReturn = true;
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match)).ToArray();
			var teams = NormalizeTeams(matchList).Length;
			var totalRounds = teams - 1 + teams % 2;
			totalRounds *= 2;
			var matchesPerRound = matchList.Length / totalRounds;
			var approved = true;
			for (var i = 1; i < totalRounds; i++)
			{
				var matchesInRound = matchList.Where(match => match.Round == i).Count();
				if (matchesPerRound != matchesInRound)
				{
					approved = false;
					break;
				}
			}
			Assert.True(approved);
		}

		[Theory]
		[ClassData(typeof(RoundRobinSetup))]
		public void PrintResult(Group group, bool hasReturn, bool isMirror)
		{
			group.RoundRobinMatches(hasReturn, isMirror);
			MatchShortFormat[] matchList = group.Matchs
				.Select(match => new MatchShortFormat(match))
				.OrderBy(match => match.Round).ToArray();
			var round = 0;
			foreach (var match in matchList)
			{
				if (round != match.Round)
				{
					round = match.Round;
					_output.WriteLine("");
					_output.WriteLine($"---------------- RODADA {round} ----------------");
				}
				_output.WriteLine(match.ToString());
			}
			Assert.True(true);
		}
	}
}
