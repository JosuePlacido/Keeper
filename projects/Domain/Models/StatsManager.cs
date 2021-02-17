using Keeper.Domain.Core;

namespace Keeper.Domain.Models
{
	public class StatsManager : Entity
	{
		public int Games { get; protected set; }
		public int Won { get; protected set; }
		public int Drowns { get; protected set; }
		public int Lost { get; protected set; }
		private int _goalsScores;
		public int GoalsScores
		{
			get { return _goalsScores; }
			protected set
			{
				_goalsScores = value;
				GoalsDifference = GoalsScores - GoalsAgainst;
			}
		}

		private int _goalsAgainst;

		public int GoalsAgainst
		{
			get { return _goalsAgainst; }
			protected set
			{
				_goalsAgainst = value;
				GoalsDifference = GoalsScores - GoalsAgainst;
			}
		}
		public int GoalsDifference { get; protected set; }
		public int Yellows { get; protected set; }
		public int Reds { get; protected set; }


		public StatsManager(string id) : base(id) { }
		public StatsManager() : base() { }

		public virtual void RegisterResult(int goalsScore, int goalsAgainst)
		{
			Games++;
			GoalsScores += goalsScore;
			GoalsAgainst += goalsAgainst;

			if (goalsScore == goalsAgainst)
			{
				Drowns++;
			}
			else if (goalsScore > goalsAgainst)
			{
				Won++;
			}
			else
			{
				Lost++;
			}
		}

		public virtual void UpdateResult(int goalsScoredDifference, int goalsAgainstDifference,
			int oldResult, int newResult)
		{
			GoalsScores += goalsScoredDifference;
			GoalsAgainst += goalsAgainstDifference;

			Won += newResult > 0 && oldResult <= 0 ? 1 :
				(newResult < 0 && oldResult > 0) ? -1 : 0;

			Drowns += newResult == 0 && oldResult != 0 ? 1 :
				(newResult != 0 && oldResult == 0) ? -1 : 0;

			Lost += newResult < 0 && oldResult >= 0 ? 1 :
				(newResult >= 0 && oldResult < 0) ? -1 : 0;
		}

		public virtual void UpdateCards(int yellowsCards, int redCards)
		{
			Yellows += yellowsCards;
			Reds += redCards;
		}
	}
}
