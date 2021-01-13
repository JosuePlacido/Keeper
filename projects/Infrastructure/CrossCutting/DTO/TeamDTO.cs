using System.ComponentModel.DataAnnotations;

namespace Infrastructure.CrossCutting.DTO
{
	public class TeamCreateDTO
	{
		[Required(ErrorMessage = "Campo obrigatório")]
		[MaxLength(50, ErrorMessage = "Máximo de 50")]
		public string Name { get; set; }

		[MaxLength(5, ErrorMessage = "5")]
		public string Abrev { get; set; }
		[DataType(DataType.Url)]
		[MaxLength(200, ErrorMessage = "Máximo de 200")]
		public string LogoUrl { get; set; }
	}
	public class TeamUpdateDTO
	{
		[Required(ErrorMessage = "Campo obrigatório")]
		public string Id { get; set; }

		[MaxLength(100, ErrorMessage = "Máximo de 100")]
		public string Name { get; set; }
		[MaxLength(5, ErrorMessage = "5")]
		public string Abrev { get; set; }
		[DataType(DataType.Url)]
		[MaxLength(200, ErrorMessage = "Máximo de 200")]
		public string LogoUrl { get; set; }
	}
}
