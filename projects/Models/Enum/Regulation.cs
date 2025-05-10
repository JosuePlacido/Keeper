using System.ComponentModel.DataAnnotations;
namespace Models.Enum
{
    public enum Classificados
    {
        [Display(Name = "Sorteio")]
        Random,
        [Display(Name = "Melhores vs Piores")]
        BestVsWorst,
        [Display(Name = "Cruzamento pre definido")]
        Configured,
    }
}
