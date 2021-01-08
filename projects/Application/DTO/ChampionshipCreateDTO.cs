using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
	public class ChampionshipCreateDTO
	{
		public string Name { get; set; }
		public string Edition { get; set; }
		public string CategoryId { get; set; }
		public StageDTO[] Stages { get; set; }
		public TeamDTO[] Teams { get; set; }
	}
	public class StageDTO
	{
		public int Order { get; set; }
		public string Name { get; set; }
		public TypeStage TypeStage { get; set; }
		public bool DuplicateTurn { get; set; }
		public bool MirrorTurn { get; set; }
		public string[] Criterias { get; set; }
		public Classifieds Regulation { get; set; }
		public GroupDTO[] Groups { get; set; }

		public StageDTO()
		{
		}
	}

	public class GroupDTO
	{
		public string Name { get; set; }
		public VacancyDTO[] Vacancys { get; set; }
		public string[] Teams { get; set; }
		public GroupDTO() { }
	}

	public class VacancyDTO
	{
		public string Description { get; set; }
		public int FromStageIndex { set; get; }
		public Classifieds OcupationType { get; set; }
		public int FromGroupIndex { set; get; }
		public int? FromPosition { get; set; }
	}

	public class TeamDTO
	{
		public string TeamId { get; set; }
		public string[] Players { get; set; }
		public TeamDTO() { }
	}
}
