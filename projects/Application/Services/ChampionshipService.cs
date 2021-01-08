using Application.DTO;
using Application.Interface;
using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Repository;

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
			for (int s = 0; s < championship.Stages.Count(); s++)
			{
				var stage = championship.Stages.ElementAt(s);
				for (int g = 0; g < stage.Groups.Count(); g++)
				{
					var group = stage.Groups.ElementAt(g);
					for (int x = 0; x < group.Statistics.Count(); x++)
					{
						var teamId = group.Statistics.ElementAt(x).TeamSubscribeId;
						group.Statistics.ElementAt(x).TeamSubscribeId = championship.Teams.Where(team => team.TeamId == teamId)
							.FirstOrDefault().Id;
					}
					if (group.Vacancys != null)
					{
						for (int x = 0; x < group.Vacancys.Count(); x++)
						{
							var vacancy = group.Vacancys.ElementAt(x);
							var indexGroup = dto.Stages[s].Groups[g].Vacancys[x].FromGroupIndex;
							vacancy.FromGroupId = championship.Stages.ElementAt(vacancy.FromStageOrder).Groups
								.ElementAt(indexGroup).Id;
						}
					}				
					group.RoundRobinMatches(dto.Stages[s].DuplicateTurn, dto.Stages[s].MirrorTurn);
				}
			}
			_repoChamp.Add(championship);
			return championship;
		}

		private Championship UpdatadeReferences(Championship championship, ChampionshipCreateDTO dto)
		{
			for (int s = 0; s < championship.Stages.Count(); s++)
			{
				var stage = championship.Stages.ElementAt(s);
				for (int g = 0; g < stage.Groups.Count(); g++)
				{
					var group = stage.Groups.ElementAt(g);
					for (int x = 0; x < group.Statistics.Count(); x++)
					{
						var teamId = group.Statistics.ElementAt(x).TeamSubscribeId;
						group.Statistics.ElementAt(x).TeamSubscribeId = championship.Teams.Where(team => team.TeamId == teamId)
							.FirstOrDefault().Id;
					}
					if (group.Vacancys != null)
					{
						for (int x = 0; x < group.Vacancys.Count(); x++)
						{
							var vacancy = group.Vacancys.ElementAt(x);
							var indexGroup = dto.Stages[s].Groups[g].Vacancys[x].FromGroupIndex;
							vacancy.FromGroupId = championship.Stages.ElementAt(vacancy.FromStageOrder).Groups
								.ElementAt(indexGroup).Id;
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
