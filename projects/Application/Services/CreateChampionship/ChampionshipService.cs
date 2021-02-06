using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using System.Linq;
using Keeper.Application.DTO;
using Keeper.Domain.Models;
using Keeper.Application.Contract;
using FluentValidation.Results;
using Keeper.Domain.Core;

namespace Keeper.Application.Services.CreateChampionship
{
	public class ChampionshipService : IChampionshipService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly IRepositoryChampionship _repoChamp;
		public ChampionshipService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
			_repoChamp = ((IRepositoryChampionship)_uow.GetDAO(typeof(IRepositoryChampionship)));
		}
		public async Task<IServiceResponse> Create(ChampionshipCreateDTO dto)
		{
			ServiceResponse response = new ServiceResponse();
			var championship = _mapper.Map<Championship>(dto);
			response.ValidationResult = new CreateChampionshipValidation()
				.ValidateScope()
				.Validate(championship);
			if (response.ValidationResult.IsValid)
			{
				championship = UpdatadeReferences(championship, dto);
				foreach (var stage in championship.Stages)
				{
					foreach (var group in stage.Groups)
					{
						group.RoundRobinMatches(stage.DuplicateTurn, stage.MirrorTurn);
					}
				}
				response.ValidationResult = new CreateChampionshipValidation()
					.ValidateSecond()
					.Validate(championship);
				using (var daoPlayer = ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer))))
				{
					var daoTeam = ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam)));
					string[] teamInvalids = await daoTeam.Exists(championship.Teams.Select(ts => ts.TeamId).ToArray());
					string[] playerInvalids = await daoPlayer.Exists(championship
						.Teams.SelectMany(ts => ts.Players.Select(p => p.PlayerId)).ToArray());
					if (teamInvalids.Length != 0)
					{
						foreach (var team in teamInvalids)
						{
							response.ValidationResult.Errors.Add(new ValidationFailure(
								"Teams", $"O time ({team}) não registrado"
							));

						}
					}
					if (playerInvalids.Length != 0)
					{
						foreach (var player in playerInvalids)
						{
							response.ValidationResult.Errors.Add(new ValidationFailure(
								"Player", $"O joogador ({player}) não registrado"
							));
						}
					}
					if (response.ValidationResult.IsValid)
					{
						championship = await _repoChamp.Add(championship);
						await _uow.Commit();
						response.Value = _mapper.Map<MatchEditsScope>(championship);
					}
				}
			}
			return response;
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
