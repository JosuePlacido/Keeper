using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using System.Linq;

namespace Keeper.Infrastructure.CrossCutting.Adapter
{
	public class MatchEditProfile : Profile
	{
		public MatchEditProfile()
		{
			CreateMap<Championship, MatchEditsScope>()
				.ForMember(dest => dest.Errors, opt => opt.Ignore());
			CreateMap<Match, MatchItemDTO>()
				.ForMember(dest => dest.HasError, opt => opt.MapFrom(src => false));
			CreateMap<Stage, MatchStageEdit>();
			CreateMap<Group, MatchGroupEdit>()
				.AfterMap((src, dest) => dest.Matchs = dest.Matchs.OrderBy(m => m.Round).ToArray());
			CreateMap<Vacancy, MatchEditVacancy>();
			CreateMap<TeamSubscribe, MatchEditTeam>()
			.ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team.Name));
		}
	}
}
