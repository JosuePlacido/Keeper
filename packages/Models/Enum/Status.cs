using System.ComponentModel.DataAnnotations;
namespace Models.Enum
{
    public enum Status
    {
        [Display(Name = "Disputando")]
        Matching,
        [Display(Name = "Marcada")]
        Scheduled,
        [Display(Name = "Encerrado")]
        Finish,
        [Display(Name = "Cancelado")]
        Canceled,
        [Display(Name = "Eliminado")]
        Eliminated,
        [Display(Name = "Classificado")]
        Classified,
        [Display(Name = "Campeão")]
        Champion,
        [Display(Name = "Criado")]
        Created
    }
}
