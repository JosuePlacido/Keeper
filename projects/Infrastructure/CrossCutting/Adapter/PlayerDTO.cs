using AutoMapper;
using Domain.Models;
using Application.Services.CRUDPlayer;

namespace Infrastructure.CrossCutting.Adapter
{
	public class PlayerDTOProfile : Profile
	{
		public PlayerDTOProfile()
		{
			CreateMap<PlayerCreateDTO, Player>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<PlayerUpdateDTO, Player>();
			CreateMap<PlayerViewDTO, Player>();
		}
	}
}
