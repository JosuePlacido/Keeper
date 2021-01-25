using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Keeper.Domain.Core;
using Keeper.Domain.Models;

namespace Keeper.Domain.Enum
{
	/*
			TODO ConfrontoDireto,
			TODO GolVisitante
			TODOJogoDesempate,
			TODO Penaltis,
			TODO Sorteio
	*/
	public class TiebreackCriterion : Enumeration
	{
		public static TiebreackCriterion Points = new TiebreackCriterion(1,
			"Pontos", (r => r.Points), true);
		public static TiebreackCriterion Won = new TiebreackCriterion(2,
			"Vitórias", (r => r.Won), true);
		public static TiebreackCriterion GoalsBalance = new TiebreackCriterion(3,
			"Saldo de gols", (r => r.GoalsDifference), true);
		public static TiebreackCriterion Goals = new TiebreackCriterion(4,
			"Gols marcados", (r => r.GoalsScores), true);
		public static TiebreackCriterion RedCard = new TiebreackCriterion(5,
			"Cartões Vermelhos", (r => r.Reds));
		public static TiebreackCriterion YellowCard = new TiebreackCriterion(6,
			"Cartões amarelos", (r => r.Yellows));
		public static TiebreackCriterion GoalsAgainst = new TiebreackCriterion(7,
			"Gols sofridos", (r => r.GoalsAgainst));

		public Func<Statistic, int> Criterion { get; }
		private readonly bool IsDescending;
		private readonly bool IsComplex;
		private TiebreackCriterion(int id, string name, Func<Statistic, int> criterion,
			bool isDescending = false, bool isComplex = false) : base(id, name)
		{
			Criterion = criterion;
			IsDescending = isDescending;
			IsComplex = isComplex;
		}
		public static IEnumerable<TiebreackCriterion> GetAll()
		{
			return TiebreackCriterion.GetAll<TiebreackCriterion>();
		}
		public IOrderedEnumerable<Statistic> Order(Statistic[] items)
		{
			return IsDescending ? items.OrderByDescending(Criterion) : items.OrderBy(Criterion);
		}
		public IOrderedEnumerable<Statistic> Order(IOrderedEnumerable<Statistic> items)
		{
			return IsDescending ? items.ThenByDescending(Criterion) : items.ThenBy(Criterion);
		}
	}
}
