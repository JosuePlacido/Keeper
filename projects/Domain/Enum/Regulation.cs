using System.ComponentModel.DataAnnotations;
namespace Keeper.Domain.Enum
{
	public enum Classifieds
	{
		[Display(Name = "Sorteio")]
		Random,
		[Display(Name = "Melhores vs Piores")]
		BestVsWorst,
		[Display(Name = "Cruzamento pre definido")]
		Configured,
	}
}
