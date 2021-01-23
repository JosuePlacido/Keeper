using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;

namespace Keeper.Infrastructure.CrossCutting.Adapter
{
	public class TeamDTOProfile : Profile
	{
		public TeamDTOProfile()
		{
			CreateMap<TeamCreateDTO, Team>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<TeamUpdateDTO, Team>();
			CreateMap<TeamViewDTO, Team>();
		}
	}
}
