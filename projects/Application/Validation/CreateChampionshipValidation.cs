using System.Linq;
using FluentValidation;
using Keeper.Domain.Models;

namespace Keeper.Application.Interface
{
	public class CreateChampionshipValidation : AbstractValidator<Championship>
	{
		public CreateChampionshipValidation()
		{
			ValidateName();
			ValidateEdition();
			ValidateStructure();
		}


		protected void ValidateName()
		{
			RuleFor(c => c.Name)
				.NotEmpty().WithMessage("Nome é um campo obrigatório")
				.Length(2, 100).WithMessage("O nome deve ter de 2 a 100 caracteres");
		}

		protected void ValidateEdition()
		{
			RuleFor(c => c.Edition)
				.NotEmpty().WithMessage("Edição é um campo obrigatório")
				.Length(2, 50).WithMessage("O nome deve ter de 2 a 50 caracteres");
		}

		protected void ValidateStructure()
		{
			RuleFor(c => c.Stages).NotNull().NotEmpty()
				.WithMessage("O campeonato deve ter no mínimo 1 Fase");
			RuleForEach(c => c.Stages).ChildRules(stg =>
			{
				stg.RuleFor(s => s.Groups).NotNull().NotEmpty();
			}).WithMessage("O campeonato deve ter no mínimo 1 grupo");

			RuleForEach(c => c.Stages).ChildRules(stg =>
			{
				stg.RuleForEach(s => s.Groups).ChildRules(grp =>
				{
					grp.RuleFor(g => g.Matchs).NotNull().NotEmpty();
				});
			}).WithMessage("O campeonato deve ter no mínimo 1 jogo criado");

			RuleFor(c => c.Stages[0])
				.Must(s => s.Groups.SelectMany(g => g.Vacancys).Count() +
					s.Groups.SelectMany(g => g.Statistics).Count() >= 2)
				.When(c => c.Stages != null && c.Stages.Count > 0)
				.WithMessage("O campeonato deve ter no mínimo 2 vagas/times");
		}
	}
}
