using FluentValidation;

namespace Keeper.Application.Services.RegisterResult
{
	public class RegisterResultValidation : AbstractValidator<MatchResultDTO>
	{
		public RegisterResultValidation()
		{
			ValidateGoals();
			ValidateEvents();
		}
		protected void ValidateGoals()
		{
			RuleFor(m => m.GoalsHome).NotNull().WithMessage("Gols do mandante é obrigatório");
			RuleFor(m => m.GoalsAway).NotNull().WithMessage("Gols do visitante é obrigatório");
		}
		protected void ValidateEvents()
		{
			RuleForEach(m => m.Events).SetValidator(new EventGameValidator());
		}
	}
	public class EventGameValidator : AbstractValidator<EventGameDTO>
	{
		public EventGameValidator()
		{
			ValidateDescription();
			ValidateType();
			ValidateMatch();
			ValidatePlayer();
		}
		protected void ValidateDescription()
		{
			RuleFor(ev => ev.Description)
				.NotEmpty().NotNull().WithMessage("Campo obrigatório")
				.MaximumLength(200).WithMessage("Máximo de 200 caracteres");
		}
		protected void ValidateType()
		{
			RuleFor(ev => ev.Type).NotNull();
		}
		protected void ValidateMatch()
		{
			RuleFor(ev => ev.MatchId)
				.NotEmpty().NotNull().WithMessage("Campo obrigatório");
		}
		protected void ValidatePlayer()
		{
			RuleFor(ev => ev.PlayerId)
				.NotEmpty().NotNull().WithMessage("Campo obrigatório");
		}
	}
}
