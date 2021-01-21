using AutoMapper;
using Keeper.Domain.Enum;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;
using System.Linq;

namespace Keeper.Infrastructure.CrossCutting.Adapter
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
				.ForMember(dest => dest.Category, opts => opts.MapFrom(src => new Category(src.Category)))
				.ForMember(dest => dest.Status, opts => opts.MapFrom(src => Status.Created));
			CreateMap<StageDTO, Stage>()
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.ChampionshipId, opts => opts.Ignore())
				.ForMember(dest => dest.Criterias, opts => opts.MapFrom(src => string.Join(",", src.Criterias)));
			CreateMap<GroupDTO, Group>()
				.ConstructUsing(src => new Group(src.Name))
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.StageId, opts => opts.Ignore())
				.ForMember(dest => dest.Matchs, opts => opts.Ignore())
				.ForMember(dest => dest.Statistics, opts => opts.Ignore());
			CreateMap<VacancyDTO, Vacancy>()
				.ConstructUsing(src => new Vacancy(src.Description, src.OcupationType,
					src.FromStageIndex, src.FromPosition))
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.GroupId, opts => opts.Ignore())
				.ForMember(dest => dest.FromStageOrder, opts => opts.MapFrom(src => src.FromStageIndex))
				.ForMember(dest => dest.FromGroupId, opts => opts.Ignore());
			CreateMap<TeamDTO, TeamSubscribe>()
				.ConstructUsing(src => new TeamSubscribe(src.TeamId))
				.ForMember(dest => dest.Players, opts =>
					opts.MapFrom(src => src.Players.Select(str => new PlayerSubscribe(str))))
				.ForAllOtherMembers(opt => opt.Ignore());
		}
	}
}
