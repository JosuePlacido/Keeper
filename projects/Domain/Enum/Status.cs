using System.ComponentModel.DataAnnotations;
namespace Domain.Enum
{
	public static class Status
	{
		public const string Matching = "Disputando";
		public const string Scheduled = "Marcado";
		public const string Finish = "Encerrado";
		public const string Canceled = "Cancelado";
		public const string Eliminated = "Eliminado";
		public const string Classified = "Classificado";
		public const string Champion = "Campeão";
		public const string Created = "Criado";
	}
}
