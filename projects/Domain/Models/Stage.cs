using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
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

		public Stage(int order, string name, bool duplicateTurn, bool mirrorTurn,
			TypeStage typeStage, string criterias, Classifieds regulation, IList<Group> groups)
		{
			Order = order;
			Name = name;
			DuplicateTurn = duplicateTurn;
			MirrorTurn = mirrorTurn;
			TypeStage = typeStage;
			Criterias = criterias;
			Regulation = regulation;
			Groups = groups;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
