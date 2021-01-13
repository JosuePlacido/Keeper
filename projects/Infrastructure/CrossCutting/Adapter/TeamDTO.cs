using AutoMapper;
using Domain.Models;
using Infrastructure.CrossCutting.DTO;

namespace Infrastructure.CrossCutting.Adapter
{
	public class TeamDTOProfile : Profile
	{
		public TeamDTOProfile()
		{
			CreateMap<TeamCreateDTO, Team>()
				.ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<TeamUpdateDTO, Team>();
		}
	}
}
