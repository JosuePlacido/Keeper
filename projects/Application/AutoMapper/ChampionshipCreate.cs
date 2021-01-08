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
	public class ChampionshipDTOToDomainProfile : Profile
	{
		public ChampionshipDTOToDomainProfile()
		{
			CreateMap<ChampionshipCreateDTO, Championship>()
				.BeforeMap((src, dest) =>
					{
						src.Stages.Select((stage, index) =>
						{
							stage.Order = index;
							return stage;
						});
					})
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.Category, opts => opts.Ignore())
				.ForMember(dest => dest.Status, opts => opts.MapFrom(src => Status.Created));
			CreateMap<StageDTO, Stage>()
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.Championship, opts => opts.Ignore())
				.ForMember(dest => dest.ChampionshipId, opts => opts.Ignore())
				.ForMember(dest => dest.Criterias, opts => opts.MapFrom(src => string.Join(",",src.Criterias)));
			CreateMap<GroupDTO, Group>()
				.ConstructUsing(src => new Group(src.Name))
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.Stage, opts => opts.Ignore())
				.ForMember(dest => dest.StageId, opts => opts.Ignore())
				.ForMember(dest => dest.Matchs, opts => opts.Ignore())
				.ForMember(dest => dest.Statistics, opts => opts.MapFrom(src => src.Teams.Select(team => new Statistics {
					TeamSubscribeId = team
				})));
			CreateMap<VacancyDTO, Vacancy>()
				.ConstructUsing( src => new Vacancy(src.Description,src.OcupationType,null,src.FromStageIndex,src.FromPosition))
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.Group, opts => opts.Ignore())
				.ForMember(dest => dest.GroupId, opts => opts.Ignore())
				.ForMember(dest => dest.FromStageOrder, opts => opts.MapFrom( src => src.FromStageIndex))
				.ForMember(dest => dest.FromGroupId, opts => opts.Ignore());
			CreateMap<TeamDTO, TeamSubscribe>()
				.ConstructUsing((src, dest) => new TeamSubscribe(src.TeamId))
				.ForMember(dest => dest.Players, opts => opts.MapFrom(src => src.Players.Select(str => new PlayerSubscribe(str))))
				.ForAllOtherMembers(opt => opt.Ignore());
		}
	}
}
