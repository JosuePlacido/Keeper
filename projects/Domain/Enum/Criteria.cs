using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Keeper.Domain.Enum
{
	public enum Criteria
	{
		[Display(Name = "Pontos")]
		Pontos,
		[Display(Name = "Vitórias")]
		Vitoria,
		[Display(Name = "Gols marcados")]
		GolsMarcados,
		[Display(Name = "Saldo de gols")]
		SaldoGols,
		Amarelos,
		Vermelhos,
		[Display(Name = "Gols sofridos")]
		GolsSofridos,
		[Display(Name = "Confronto direto")]
		ConfrontoDireto,
		[Display(Name = "Gol fora(Somente Mata-Mata)")]
		GolsVisitante,

		JogoDesempate,
		[Display(Name = "Disputa de penaltis(Somente Mata-Mata)")]
		Penaltis,
		Sorteio
	}
}
