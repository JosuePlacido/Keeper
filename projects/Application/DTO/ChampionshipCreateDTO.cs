using System.ComponentModel.DataAnnotations;
using Keeper.Domain.Enum;

namespace Keeper.Application.DTO
{
	public class ChampionshipCreateDTO
	{
		[Required(ErrorMessage = "Campo orbigatório")]
		[MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Campo orbigatório")]
		[MaxLength(50, ErrorMessage = "Máximo de 50 caracteres")]
		public string Edition { get; set; }
		public string Category { get; set; }
		[Required(ErrorMessage = "Campo orbigatório")]
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
		public string Name { get; set; }
		public string[] Players { get; set; }
		public int InitStageOrder { get; set; }
		public int InitGroupIndex { get; set; }
		public TeamDTO() { }
	}
}
