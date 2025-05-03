using AutoMapper;
using Domain.Models;
using Application.Services.CRUDTeam;

namespace Infrastructure.CrossCutting.Adapter
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
