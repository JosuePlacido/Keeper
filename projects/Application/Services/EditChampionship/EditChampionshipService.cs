using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using System.Linq;
using System.Collections.Generic;
using Keeper.Application.DTO;
using Keeper.Domain.Models;
using Keeper.Application.Contract;
using FluentValidation.Results;
using Keeper.Domain.Enum;

namespace Keeper.Application.Services.EditChampionship
{
	public class EditChampionshipService : IEditChampionshipService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly IRepositoryChampionship _repoChamp;
		public EditChampionshipService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
			_repoChamp = ((IRepositoryChampionship)_uow.GetDAO(typeof(IRepositoryChampionship)));
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<SquadEditDTO[]> GetSquads(string championshipId)
		{
			Championship championship = await _repoChamp.GetByIdWithTeamsWithPLayers(championshipId);
			return championship != null ? _mapper.Map<SquadEditDTO[]>(championship.Teams) :
				new SquadEditDTO[0];
		}

		public async Task<IServiceResponse> UpdateSquad(PLayerSquadPostDTO[] squadsEnter)
		{
			ServiceResponse response = new ServiceResponse();
			squadsEnter = squadsEnter.Where(s => !string.IsNullOrEmpty(s.Id) ||
			!string.IsNullOrEmpty(s.TeamSubscribeId) || s.Status != Status.FreeAgent).ToArray();
			PlayerSubscribe[] squads = _mapper.Map<PlayerSubscribe[]>(squadsEnter);
			using (var daoPlayerSubscribe = ((IDAOPlayerSubscribe)_uow.GetDAO(typeof(IDAOPlayerSubscribe))))
			{
				response.ValidationResult = new ValidationResult();
				for (int x = 0; x < squads.Length; x++)
				{
					PlayerSubscribe player = squads[x];
					string errorMessage = await daoPlayerSubscribe.ValidateUpdateOnSquad(player);
					if (!string.IsNullOrEmpty(errorMessage))
					{
						response.ValidationResult.Errors.Add(
							new ValidationFailure(squadsEnter[x].PlayerName, errorMessage));
						player = await _repoChamp.UpdatePLayer(player);
					};
				}
				if (response.ValidationResult.IsValid)
				{
					await _uow.Commit();
					response.Value = squads;
				}

			}
			return response;
		}

		public async Task<ObjectRenameDTO> GetNames(string championship)
		{
			return (ObjectRenameDTO)await
				((IDAOChampionship)_uow.GetDAO(typeof(IDAOChampionship)))
				.GetByIdForRename(championship);
		}

		public async Task<IServiceResponse> RenameScopes(ObjectRenameDTO dto)
		{
			ServiceResponse response = new ServiceResponse();
			response.ValidationResult = new ValidationResult();
			if (dto == null)
			{
				response.ValidationResult.Errors.Add(new ValidationFailure("global",
					"Renomeações não enviadas"));
				return response;
			}
			Championship championship = await _repoChamp.GetByIdWithStageGroupsAndMatches(dto.Id);
			if (!string.IsNullOrEmpty(dto.Name))
			{
				championship.EditScope(dto.Name);
			}
			for (int s = 0; s < championship.Stages.Count; s++)
			{
				if (!string.IsNullOrEmpty(dto.Childs[s].Name))
				{
					championship.Stages[s].EditScope(dto.Childs[s].Name);
				}
				for (int g = 0; g < championship.Stages[s].Groups.Count; g++)
				{
					if (!string.IsNullOrEmpty(dto.Childs[s].Childs[g].Name))
					{
						championship.Stages[s].Groups[g].EditScope(dto.Childs[s].Childs[g].Name);
					}
					for (int m = 0; m < championship.Stages[s].Groups[g].Matchs.Count; m++)
					{
						if (!string.IsNullOrEmpty(dto.Childs[s].Childs[g].Childs[m].Name))
						{
							championship.Stages[s].Groups[g].Matchs[m]
								.EditScope(name: dto.Childs[s].Childs[g].Childs[m].Name);
						}
					}
				}
			}
			response.Value = await _repoChamp.RenameScopes(championship);
			await _uow.Commit();
			return response;
		}

		public async Task<RankDTO> Rank(string championship)
		{
			if (string.IsNullOrEmpty(championship))
			{
				return new RankDTO();
			}
			return _mapper.Map<RankDTO>(await _repoChamp.GetByIdWithRank(championship));
		}

		public async Task<IServiceResponse> UpdateStatistics(RankPost[] dto)
		{
			ServiceResponse response = new ServiceResponse();
			response.ValidationResult = new ValidationResult();
			if (dto == null)
			{
				response.ValidationResult.Errors.Add(
					new ValidationFailure("Id", $"Estatísticas estão em branco")
				);
			}
			List<Statistic> list = new List<Statistic>();
			if (response.ValidationResult.IsValid)
			{
				Statistic stat;
				using (var daoStat = ((IDAOStatistic)_uow.GetDAO(typeof(IDAOStatistic))))
				{
					foreach (var item in dto)
					{
						stat = await daoStat.GetById(item.Id);
						if (stat != null && response.ValidationResult.IsValid)
						{
							stat.UpdateNumbers(item.Games, item.Won, item.Drowns, item.Lost,
								item.GoalsScores, item.GoalsAgainst, item.GoalsDifference, item.Yellows,
								item.Reds, item.Points, item.Lastfive, item.Position);
						}
						else
						{
							response.ValidationResult.Errors.Add(
								new ValidationFailure("Id",
									$"Registro de estatística de id: {item.Id} não existe")
							);
						}
					}
					if (response.ValidationResult.IsValid)
					{
						response.Value = _repoChamp.UpdateStatistics(list.ToArray());
						await _uow.Commit();
					}
				}
			}
			return response;
		}

		public async Task<TeamStatisticDTO[]> TeamStats(string id)
		{
			TeamStatisticDTO[] result;
			result = _mapper.Map<TeamStatisticDTO[]>(await ((IDAOTeamSubscribe)_uow.GetDAO(typeof(IDAOTeamSubscribe)))
				.GetByChampionshipTeamStatistics(id));
			return result;
		}

		public async Task<PlayerStatisticDTO[]> PlayerStats(string id)
		{
			PlayerStatisticDTO[] result;
			result = _mapper.Map<PlayerStatisticDTO[]>(
				await ((IDAOPlayerSubscribe)_uow.GetDAO(typeof(IDAOPlayerSubscribe)))
					.GetByChampionshipPlayerStatistics(id));
			return result;
		}

		public async Task<IServiceResponse> UpdateTeamsStatistics(TeamSubscribePost[] dto)
		{
			ServiceResponse result = new ServiceResponse();
			result.ValidationResult = new ValidationResult();
			if (dto == null)
			{
				return result;
			}
			if (dto.Any(i => string.IsNullOrEmpty(i.Id)))
			{
				result.ValidationResult.Errors.Add(new ValidationFailure("Id", "Todos os ids são obrigatórios"));
			}
			TeamSubscribe[] list;
			if (result.ValidationResult.IsValid)
			{
				using (var dao = ((IDAOTeamSubscribe)_uow.GetDAO(typeof(IDAOTeamSubscribe))))
				{
					list = await dao.GetAllById(dto.Select(ts => ts.Id).ToArray());
					for (int x = 0; x < list.Length; x++)
					{
						list[x].UpdateNumbers(dto[x].Games, dto[x].Drowns, dto[x].GoalsAgainst,
							dto[x].GoalsDifference, dto[x].GoalsScores, dto[x].Lost, dto[x].Reds,
							dto[x].Won, dto[x].Yellows);
					}
					dao.UpdateAll(list);
					await _uow.Commit();
				}
				result.Value = list;
			}
			return result;
		}

		public async Task<IServiceResponse> UpdatePlayersStatistics(PlayerSubscribePost[] dto)
		{
			ServiceResponse result = new ServiceResponse();
			result.ValidationResult = new ValidationResult();
			if (dto == null)
			{
				return result;
			}
			if (dto.Any(i => string.IsNullOrEmpty(i.Id)))
			{
				result.ValidationResult.Errors.Add(new ValidationFailure("Id", "Todos os ids são obrigatórios"));
			}
			PlayerSubscribe[] list;
			if (result.ValidationResult.IsValid)
			{
				using (var dao = ((IDAOPlayerSubscribe)_uow.GetDAO(typeof(IDAOPlayerSubscribe))))
				{
					list = await dao.GetAllById(dto.Select(ts => ts.Id).ToArray());
					for (int x = 0; x < list.Length; x++)
					{
						list[x].UpdateNumbers(dto[x].Games, dto[x].Goals, dto[x].MVPs, dto[x].Reds,
							dto[x].Yellows);
					}
					dao.UpdateAll(list);
					await _uow.Commit();

				}
				result.Value = list;
			}
			return result;
		}

	}
}
