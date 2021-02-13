using System;
using System.Collections.Generic;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Keeper.Test;
using Xunit;
using Xunit.Abstractions;

namespace Keeper.Test.UnitTest.Domain
{
	public class RegisterResultOfMatch
	{
		private readonly ITestOutputHelper _output;
		public RegisterResultOfMatch(ITestOutputHelper output)
			=> _output = output;
		[Fact]
		public void TestRegisterResult()
		{
			PlayerSubscribe[] players = new PlayerSubscribe[]{
				PlayerSubscribe.Factory("ps1","p1","ts1"),
				PlayerSubscribe.Factory("ps2","p2","ts2"),
			};
			TeamSubscribe[] teams = new TeamSubscribe[]{
				TeamSubscribe.Factory("ts1","t1",players: new List<PlayerSubscribe>{players[0]}),
				TeamSubscribe.Factory("ts2","t2",players: new List<PlayerSubscribe>{players[1]}),
			};
			EventGame[] events = new EventGame[]{
				EventGame.Factory("ev1",1,"Gol mandante",TypeEvent.Goal,
					true,"m1","ps1"),
				EventGame.Factory("ev2",2,"Autogol mandante",TypeEvent.Autogol,
					true,"m1","ps1"),
				EventGame.Factory("ev3",3,"Lesão mandante",TypeEvent.Injury,
					true,"m1","ps1"),
				EventGame.Factory("ev4",4,"Amarelo mandante",TypeEvent.YellowCard,
					true,"m1","ps1"),
				EventGame.Factory("ev5",5,"Vermelho mandante",TypeEvent.RedCard,
					true,"m1","ps1"),
				EventGame.Factory("ev6",6,"MVp mandante",TypeEvent.MVP,
					true,"m1","ps1"),
				EventGame.Factory("ev7",7,"Gol mandante",TypeEvent.Goal,
					true,"m1","ps1"),
				EventGame.Factory("ev8",8,"Gol mandante",TypeEvent.Goal,
					true,"m1","ps1"),
				EventGame.Factory("ev9",9,"Gol visitante",TypeEvent.Goal,
					false,"m1","ps2"),
				EventGame.Factory("ev10",10,"Autogol visitante",TypeEvent.Autogol,
					false,"m1","ps2"),
				EventGame.Factory("ev11",11,"Lesão visitante",TypeEvent.Injury,
					false,"m1","ps2"),
				EventGame.Factory("ev12",12,"Amarelo visitante",TypeEvent.YellowCard,
					false,"m1","ps2"),
				EventGame.Factory("ev13",13,"Vermelho visitante",TypeEvent.RedCard,
					false,"m1","ps2"),
				EventGame.Factory("ev14",14,"MVp visitante",TypeEvent.MVP,
					false,"m1","ps2"),
				EventGame.Factory("ev15",15,"Gol visitante",TypeEvent.Goal,
					false,"m1","ps2")
			};
			Match match = Match.Factory("m1", round: 1, groupId: "g1", name: "Final Ida",
				date: new DateTime(2021, 9, 26, 21, 00, 00), address: "Morumbi - São Paulo/SP",
				aggregateGame: true, home: teams[0], away: teams[1]);
			match.RegisterResult(3, 2, events: events);
			Assert.Equal(3, match.Home.Players[0].Goals);
			Assert.Equal(1, match.Home.Players[0].Games);
			Assert.Equal(1, match.Home.Players[0].YellowCard);
			Assert.Equal(1, match.Home.Players[0].RedCard);
			Assert.Equal(1, match.Home.Players[0].MVPs);
			Assert.Equal(2, match.Away.Players[0].Goals);
			Assert.Equal(1, match.Away.Players[0].Games);
			Assert.Equal(1, match.Away.Players[0].YellowCard);
			Assert.Equal(1, match.Away.Players[0].RedCard);
			Assert.Equal(1, match.Away.Players[0].MVPs);

			Assert.Equal(Status.Finish, match.Status);
			Assert.Equal(3, match.GoalsHome);
			Assert.Equal(3, match.AggregateGoalsHome);
			Assert.Equal(2, match.GoalsAway);
			Assert.Equal(2, match.AggregateGoalsAway);

			Assert.Equal(1, match.Home.Games);
			Assert.Equal(1, match.Home.Won);
			Assert.Equal(0, match.Home.Drowns);
			Assert.Equal(0, match.Home.Lost);
			Assert.Equal(3, match.Home.GoalsScores);
			Assert.Equal(2, match.Home.GoalsAgainst);
			Assert.Equal(1, match.Home.GoalsDifference);
			Assert.Equal(1, match.Home.Yellows);
			Assert.Equal(1, match.Home.Reds);
			Assert.Equal(1, match.Away.Games);
			Assert.Equal(0, match.Away.Won);
			Assert.Equal(0, match.Away.Drowns);
			Assert.Equal(1, match.Away.Lost);
			Assert.Equal(2, match.Away.GoalsScores);
			Assert.Equal(3, match.Away.GoalsAgainst);
			Assert.Equal(-1, match.Away.GoalsDifference);
			Assert.Equal(1, match.Away.Yellows);
			Assert.Equal(1, match.Away.Reds);
		}
	}
}
