using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Application.DTO;

namespace Keeper.Infrastructure.CrossCutting.Adapter
{
	public class SingleStatisticDTOProfile : Profile
	{
		public SingleStatisticDTOProfile()
		{
			CreateMap<TeamSubscribe, TeamStatisticDTO>()
				.ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team.Name));
			CreateMap<PlayerSubscribe, PlayerStatisticDTO>()
				.ForMember(dest => dest.Player, opt => opt.MapFrom(src => src.Player.Name));
		}
	}
}
