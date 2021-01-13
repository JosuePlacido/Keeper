using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Infrastructure.CrossCutting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Adapter
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
