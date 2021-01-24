using FluentValidation.Results;
using Keeper.Application.DTO;

namespace Keeper.Application.Models
{
	public class CreateChampionshipResponse : ValidationResult
	{
		public CreateChampionshipResponse(ValidationResult validationResult)
		{
			ValidationResult = validationResult;
		}

		public ValidationResult ValidationResult { get; set; }
		public MatchEditsScope Matches { get; set; }
	}
}
