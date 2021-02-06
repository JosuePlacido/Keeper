using AutoMapper;
using Keeper.Domain.Models;
using Keeper.Application.Services.EditChampionship;

namespace Keeper.Infrastructure.CrossCutting.Adapter
{
	public class RankDTOProfile : Profile
	{
		public RankDTOProfile()
		{
			CreateMap<Championship, RankDTO>();
			CreateMap<Stage, StageRankDTO>();
			CreateMap<Group, GroupRankDTO>();
			CreateMap<Statistic, RankModel>()
				.ForMember(dst => dst.Team, opt => opt.MapFrom(src => src.TeamSubscribe.Team.Name));
		}
	}
}
