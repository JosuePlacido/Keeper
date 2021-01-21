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
			RuleFor(c => c.Stages.Count)
				.GreaterThan(0).WithMessage("O campeonato deve ter no mínimo 1 Fase");
			RuleForEach(c => c.Stages).ChildRules(stg =>
			{
				stg.RuleFor(s => s.Groups.Count).GreaterThan(0);
			}).WithMessage("O campeonato deve ter no mínimo 1 grupo");

			RuleForEach(c => c.Stages).ChildRules(stg =>
			{
				stg.RuleForEach(s => s.Groups).ChildRules(grp =>
				{
					grp.RuleFor(g => g.Vacancys.Count).GreaterThan(1);
				});
			}).When(c => c.Teams.Count < 2)
				.WithMessage("O campeonato deve ter no mínimo com 2 vagas ou 2 times inscritos");

			RuleForEach(c => c.Stages).ChildRules(stg =>
			{
				stg.RuleForEach(s => s.Groups).ChildRules(grp =>
				{
					grp.RuleFor(g => g.Matchs.Count).GreaterThan(0);
				});
			}).WithMessage("O campeonato deve ter no mínimo 1 jogo criado");
		}
	}
}
