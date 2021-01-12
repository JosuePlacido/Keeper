using Application.DTO;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
	public class MatchEditProfile : Profile
	{
		public MatchEditProfile()
		{
			CreateMap<Match, MatchItemDTO>()
				.ForMember(dest => dest.Errors, opt => opt.Ignore());
			CreateMap<Stage, MatchEditsScope>();
			CreateMap<Group, MatchGroupEdit>()
				.AfterMap((src, dest) => dest.Matchs = dest.Matchs.OrderBy(m => m.Round).ToArray());
		}
	}
}
