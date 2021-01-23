using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Infrastructure.CrossCutting.Adapter
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
