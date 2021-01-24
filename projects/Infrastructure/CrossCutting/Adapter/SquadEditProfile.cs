using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Application.DTO;

namespace Keeper.Infrastructure.CrossCutting.Adapter
{
	public class SquadEditDTOProfile : Profile
	{
		public SquadEditDTOProfile()
		{
			CreateMap<TeamSubscribe, SquadEditDTO>();
			CreateMap<PlayerSubscribe, PLayerSquadEditDTO>()
				.ForMember(dst => dst.PlayerName, opt => opt.MapFrom(src =>
					   src.Player.Name))
				.ForMember(dst => dst.PlayerNick, opt => opt.MapFrom(src =>
					   src.Player.Nickname));
		}
	}
}
