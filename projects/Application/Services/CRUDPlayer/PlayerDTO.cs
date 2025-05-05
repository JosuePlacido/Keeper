using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.Services.CRUDPlayer;
public class PlayerCreateDTO
{
	[Required(ErrorMessage = "Campo obrigatório")]
	[MaxLength(50, ErrorMessage = "Máximo de 50")]
	public string Name { get; set; }
	[MaxLength(50, ErrorMessage = "Máximo de 50")]
	public string Nickname { get; set; }
}
public class PlayerUpdateDTO
{
	[Required(ErrorMessage = "Campo obrigatório")]
	public string Id { get; set; }

	[MaxLength(100, ErrorMessage = "Máximo de 100")]
	[Required(ErrorMessage = "Campo obrigatório")]
	public string Name { get; set; }
	[MaxLength(50, ErrorMessage = "Máximo de 50")]
	public string Nickname { get; set; }
}
public class PlayerViewDTO
{
	public string Id { get; set; }
	public string Name { get; set; }
	public string Nickname { get; set; }
	public bool IsDeletable { get; set; }
}
public class PlayerAvailablePaginationDTO
{
	public Player[] Players { get; set; }
	public string Terms { get; set; }
	public string ExcludeFromChampionship { get; set; }
	public int Page { get; set; }
	public int Take { get; set; }
	public int Total { get; set; }

	public override string ToString()
	{
		return $"Terms: {Terms}, ExcludeFromChampionship: {ExcludeFromChampionship}, Page: {Page}, Total: {Total}";
	}
}
