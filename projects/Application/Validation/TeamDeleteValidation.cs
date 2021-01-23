using FluentValidation;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Application.Validation
{
	public class TeamDeleteValidation : AbstractValidator<TeamViewDTO>
	{
		public TeamDeleteValidation()
		{
			ValidateObject();
		}


		protected void ValidateObject()
		{
			RuleFor(t => t.Id)
				.NotEmpty().WithMessage("Time não registrado");
			RuleFor(t => t.IsDeletable)
				.Must(id => true).WithMessage("Time inscrito em campeonato");
		}
	}
}
