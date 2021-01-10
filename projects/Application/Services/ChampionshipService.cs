using Application.DTO;
using Application.Interface;
using AutoMapper;
using FluentValidation.Results;
using System;
using System.Text;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Repository;
using System.Linq;

namespace Application.Services
{
	public class ChampionshipService : IChampionshipService
	{
		private readonly IRepositoryChampionship _repoChamp;
		private readonly IMapper _mapper;
		public ChampionshipService(IMapper mapper, IRepositoryChampionship repoChamp)
		{
			_mapper = mapper;
			_repoChamp = repoChamp;
		}
		public Championship Create(ChampionshipCreateDTO dto)
		{
			var championship = _mapper.Map<Championship>(dto);
			championship = UpdatadeReferences(championship, dto);
			foreach (var stage in championship.Stages)
			{
				foreach (var group in stage.Groups)
				{
					group.RoundRobinMatches(stage.DuplicateTurn, stage.MirrorTurn);
				}
			}
			_repoChamp.Add(championship);
			return championship;
		}

		private Championship UpdatadeReferences(Championship championship, ChampionshipCreateDTO dto)
		{
			foreach (var team in dto.Teams)
			{
				var teamSubscribeId = championship.Teams.Where(t => t.TeamId == team.TeamId)
					.FirstOrDefault().Id;
				championship.Stages[team.InitStageOrder].Groups[team.InitGroupIndex]
					.AddTeam(teamSubscribeId);
			}
			for (int s = 0; s < championship.Stages.Count(); s++)
			{
				var stage = championship.Stages[s];
				for (int g = 0; g < stage.Groups.Count; g++)
				{
					var group = stage.Groups[g];
					if (group.Vacancys != null)
					{
						for (int x = 0; x < group.Vacancys.Count; x++)
						{
							var vacancy = group.Vacancys[x];
							if (vacancy.FromStageOrder != null)
							{
								var indexGroup = dto.Stages[s].Groups[g].Vacancys[x].FromGroupIndex;
								var indexOfGroup = championship.Stages[(int)vacancy.FromStageOrder]
									.Groups[indexGroup].Id;
								vacancy.AddReferenceFromGroup(indexOfGroup);
							}
						}
					}
				}
			}
			return championship;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
