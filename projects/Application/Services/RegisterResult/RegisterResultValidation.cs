using Domain.Models;
using FluentValidation;

namespace Application.Services.RegisterResult
{
	public class RegisterResultValidation : AbstractValidator<MatchResultDTO>
	{
		public RegisterResultValidation(Match match)
		{
			ValidateGoals();
			ValidateEvents();
			ValidatePenaltiesIfRequired(match);
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
		protected void ValidatePenaltiesIfRequired(Match match)
		{
			RuleFor(m => m).Custom((dto, context) =>
			{
				int totalHome = dto.GoalsHome + (match.AggregateGoalsHome ?? 0);
				int totalAway = dto.GoalsAway + (match.AggregateGoalsAway ?? 0);

				if (match.FinalGame && match.Penalty && totalHome == totalAway)
				{
					if (dto.GoalsPenaltyHome == dto.GoalsPenaltyAway &&
						dto.GoalsPenaltyHome != null)
					{
						context.AddFailure("GoalsPenaltyHome", "Gols em penaltis dos times precisam ser diferentes");
						context.AddFailure("GoalsPenaltyAway", "Gols em penaltis dos times precisam ser diferentes");
					}

					if (dto.GoalsPenaltyHome == null)
						context.AddFailure("GoalsPenaltyHome", "Gols em penaltis do mandante são obrigatórios");

					if (dto.GoalsPenaltyAway == null)
						context.AddFailure("GoalsPenaltyAway", "Gols em penaltis do visitante são obrigatórios");
				}
			});
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
