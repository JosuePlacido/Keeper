using FluentValidation;
using Keeper.Application.DTO;

namespace Application.Validation
{
	public class PlayerDeleteValidation : AbstractValidator<PlayerViewDTO>
	{
		public PlayerDeleteValidation()
		{
			ValidateObject();
		}


		protected void ValidateObject()
		{
			RuleFor(t => t.Id)
				.NotEmpty().WithMessage("Jogador não registrado");
			RuleFor(t => t.IsDeletable)
				.Must(id => true).WithMessage("Jogador inscrito em campeonato");
		}
	}
}
