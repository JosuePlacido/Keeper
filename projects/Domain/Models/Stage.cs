using Keeper.Domain.Core;
using Keeper.Domain.Enum;
using System.Collections.Generic;

namespace Keeper.Domain.Models
{
	public class Stage : Entity
	{
		public int Order { get; private set; }
		public string Name { get; private set; }
		public bool DuplicateTurn { get; set; }
		public bool MirrorTurn { get; set; }
		public TypeStage TypeStage { get; private set; }
		public string Criterias { get; private set; }
		public Classifieds Regulation { get; private set; }
		public string ChampionshipId { get; private set; }
		public IList<Group> Groups { get; private set; }
		private Stage() { }

		public override string ToString()
		{
			return Name;
		}

		public static Stage Factory(string id, string championshipId, int order, string name, bool duplicateTurn = false,
			bool mirrorTurn = false, TypeStage typeStage = TypeStage.League, string criterias = "",
			Classifieds regulation = Classifieds.Random, IList<Group> groups = null)
		{
			return new Stage
			{
				Id = id,
				Order = order,
				Name = name,
				DuplicateTurn = duplicateTurn,
				MirrorTurn = mirrorTurn,
				TypeStage = typeStage,
				Criterias = criterias,
				Regulation = regulation,
				ChampionshipId = championshipId,
				Groups = groups,
			};
		}
	}
}
