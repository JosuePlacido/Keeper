using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Enum
{
	public enum TypeStage
	{
		[Display(Name = "Eliminatória")]
		Knockout,
		[Display(Name = "Pontos corridos")]
		League,
	}
}
