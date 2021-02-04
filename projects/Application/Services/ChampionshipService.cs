using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using System.Linq;
using System.Collections.Generic;
using Keeper.Application.DTO;
using Keeper.Domain.Models;
using Keeper.Application.Interface;
using FluentValidation.Results;
using Keeper.Application.Models;
using Domain.Core;
using Keeper.Domain.Enum;
using Keeper.Application.DAO;

namespace Keeper.Application.Services
{
	public class ChampionshipService : IChampionshipService
	{
		private readonly IRepositoryChampionship _repoChamp;
		private readonly IMapper _mapper;
		private readonly IDAOPlayerSubscribe _daoPlayerSubscribe;
		private readonly IDAOTeamSubscribe _daoTeamSubscribe;
		private readonly IDAOPlayer _daoPlayer;
		private readonly IDAOStatistic _daoStat;
		private readonly IDAOTeam _daoTeam;
		private readonly IDAOChampionship _dao;
		private readonly IUnitOfWork _uow;
		public ChampionshipService(IMapper mapper, IUnitOfWork uow,
			IRepositoryChampionship repoChamp, IDAOPlayerSubscribe daoPlayerSubscribe,
			IDAOPlayer daoPlayer, IDAOTeam daoTeam, IDAOChampionship dao, IDAOStatistic daoStat,
			IDAOTeamSubscribe daoTeamSubscribe)
		{
			_mapper = mapper;
			_repoChamp = repoChamp;
			_uow = uow;
			_daoPlayerSubscribe = daoPlayerSubscribe;
			_daoPlayer = daoPlayer;
			_daoTeam = daoTeam;
			_dao = dao;
			_daoStat = daoStat;
			_daoTeamSubscribe = daoTeamSubscribe;
		}
		public async Task<IServiceResult> Create(ChampionshipCreateDTO dto)
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
				string[] teamInvalids = await _daoTeam.Exists(championship.Teams.Select(ts => ts.TeamId).ToArray());
				string[] playerInvalids = await _daoPlayer.Exists(championship
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
			return response;
		}

		public MatchEditsScope CheckMatches(MatchEditsScope dto)
		{
			if (dto.Errors == null)
			{
				dto.Errors = new List<string>();
			}
			var dictionaryTeam = new Dictionary<string, AuditoryMatch>();
			foreach (var group in dto.Stages.SelectMany(stg => stg.Groups))
			{
				dictionaryTeam.Clear();
				var matches = group.Matchs.OrderBy(mtc => mtc.Round).ToArray();
				var teams = matches.SelectMany(mtc => new string[]{
					mtc.AwayId, mtc.VacancyHomeId,
					mtc.HomeId,mtc.VacancyAwayId
				}).Distinct().Where(str => !string.IsNullOrEmpty(str)).ToArray();

				foreach (var team in teams)
				{
					dictionaryTeam.Add(team, new AuditoryMatch());
				}

				var totalMatchsPerTeam = teams.Length - 1;
				var finalRound = totalMatchsPerTeam + teams.Length % 2;
				if (dto.Stages.Where(stg => stg.Id == group.StageId).FirstOrDefault().DuplicateTurn)
				{
					finalRound *= 2;
					totalMatchsPerTeam *= 2;
				}

				string home, away = "";
				List<string> idsMatchesHasError, errorsTemp;
				foreach (var match in matches)
				{
					if (matches.Where(mtc => mtc.Equals(match) && mtc.Id != match.Id).FirstOrDefault() != null)
					{
						match.HasError = true;
						dto.Errors.Add(MatchAuditoryContants.GenerateMessage(
							MatchAuditoryContants.UNIQUE_MATCH,
							match.ToString()));
					}
					home = string.IsNullOrEmpty(match.HomeId) ? match.VacancyHomeId : match.HomeId;
					away = string.IsNullOrEmpty(match.AwayId) ? match.VacancyAwayId : match.AwayId;
					UpdateTeamAuditory("h", home, match, dictionaryTeam,
						finalRound, totalMatchsPerTeam, out errorsTemp, out idsMatchesHasError);
					((List<string>)dto.Errors).AddRange(errorsTemp);
					foreach (var mtc in matches.Where(mtc => idsMatchesHasError.Contains(mtc.Id)))
					{
						mtc.HasError = true;
					}
					UpdateTeamAuditory("a", away, match, dictionaryTeam,
						finalRound, totalMatchsPerTeam, out errorsTemp, out idsMatchesHasError);
					((List<string>)dto.Errors).AddRange(errorsTemp);
					foreach (var mtc in matches.Where(mtc => idsMatchesHasError.Contains(mtc.Id)))
					{
						mtc.HasError = true;
					}
				}
				var teamsIvalid = dictionaryTeam.Where(t => t.Value.TotalMatches != totalMatchsPerTeam)
					.ToArray();
				if (teamsIvalid.Length > 0)
				{
					foreach (var team in teamsIvalid)
					{
						dto.Errors.Add(MatchAuditoryContants.GenerateMessage(
								MatchAuditoryContants.MATCH_COUNT,
								team.Key,
								team.Value.TotalMatches
						));
					}
				}

			}
			return dto;
		}

		private Dictionary<string, AuditoryMatch> UpdateTeamAuditory(string mode, string key,
			MatchItemDTO match, Dictionary<string, AuditoryMatch> dictionary,
			int finalRound, int matchesPerTeam, out List<string> erros, out List<string> idMatches)
		{
			idMatches = new List<string>();
			erros = new List<string>();
			dictionary[key].TotalMatches++;
			if (mode == "h")
				dictionary[key].TotalHomeMatches++;
			else
				dictionary[key].TotalAwayMatches++;
			dictionary[key].HomAwayGap += (mode == "h") ? 1 : -1;
			if (dictionary[key].LastField == mode)
			{
				dictionary[key].SameFieldSequencie++;
			}
			else
			{
				if (dictionary[key].SameFieldSequencie > 2)
				{
					erros.Add(MatchAuditoryContants.GenerateMessage(
						MatchAuditoryContants.SEQUENCE_SAME_FIELD,
						TeamOfMatch(match, mode),
						dictionary[key].SameFieldSequencie,
						"#" + dictionary[key].LastField,
						string.Join(",", dictionary[key].MatchesRoundSequence
						)
					));
					idMatches.AddRange(dictionary[key].MatchesIdSequence);
				}
				dictionary[key].SameFieldSequencie = 1;
				dictionary[key].LastField = mode;
				dictionary[key].MatchesIdSequence.Clear();
				dictionary[key].MatchesRoundSequence.Clear();
			}
			dictionary[key].MatchesIdSequence.Add(match.Id);
			dictionary[key].MatchesRoundSequence.Add(match.Round);

			if (finalRound == match.Round)
			{
				if (dictionary[key].SameFieldSequencie > 2)
				{
					erros.Add(MatchAuditoryContants.GenerateMessage(
						MatchAuditoryContants.SEQUENCE_SAME_FIELD,
						TeamOfMatch(match, mode),
						dictionary[key].SameFieldSequencie,
						"#" + dictionary[key].LastField,
						string.Join(",", dictionary[key].MatchesRoundSequence
						)
					));
					idMatches.AddRange(dictionary[key].MatchesIdSequence);
				}
				if (dictionary[key].TotalMatches != matchesPerTeam)
				{
					erros.Add(MatchAuditoryContants.GenerateMessage(
							MatchAuditoryContants.MATCH_COUNT,
							TeamOfMatch(match, mode),
							dictionary[key].TotalMatches
					));
				}
				if (Math.Abs(dictionary[key].HomAwayGap) > 1)
				{

					var teamName = "";
					if (mode == "h")
					{
						teamName = string.IsNullOrEmpty(match.HomeId) ?
						match.VacancyHome.Description : match.Home.Team;
					}
					else
					{
						teamName = string.IsNullOrEmpty(match.AwayId) ?
						match.VacancyAway.Description : match.Away.Team;
					}
					erros.Add(MatchAuditoryContants.GenerateMessage(
						MatchAuditoryContants.HOME_AWAY_GAP,
						teamName,
						dictionary[key].TotalHomeMatches,
						dictionary[key].TotalAwayMatches
					));
				}
			}

			return dictionary;
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
		private string TeamOfMatch(MatchItemDTO match, string mode)
		{
			if (mode == "h")
			{
				return string.IsNullOrEmpty(match.HomeId) ?
				match.VacancyHome.Description : match.Home.Team;
			}
			else
			{
				return string.IsNullOrEmpty(match.AwayId) ?
				match.VacancyAway.Description : match.Away.Team;
			}
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<SquadEditDTO[]> GetSquads(string championshipId)
		{
			Championship championship = await _repoChamp.GetByIdWithTeamsWithPLayers(championshipId);
			return championship != null ? _mapper.Map<SquadEditDTO[]>(championship.Teams) : new SquadEditDTO[0];
		}

		public async Task<IServiceResult> UpdateSquad(PLayerSquadPostDTO[] squadsEnter)
		{
			ServiceResponse response = new ServiceResponse();
			squadsEnter = squadsEnter.Where(s => !string.IsNullOrEmpty(s.Id) ||
			!string.IsNullOrEmpty(s.TeamSubscribeId) || s.Status != Status.FreeAgent).ToArray();
			PlayerSubscribe[] squads = _mapper.Map<PlayerSubscribe[]>(squadsEnter);
			response.ValidationResult = new ValidationResult();
			for (int x = 0; x < squads.Length; x++)
			{
				PlayerSubscribe player = squads[x];
				string errorMessage = await _daoPlayerSubscribe.ValidateUpdateOnSquad(player);
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
			return response;
		}

		public async Task<ObjectRenameDTO> GetNames(string championship)
		{
			return (ObjectRenameDTO)await _dao.GetByIdForRename(championship);
		}

		public async Task<IServiceResult> RenameScopes(ObjectRenameDTO dto)
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
								.EditScope(dto.Childs[s].Childs[g].Childs[m].Name);
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

		public async Task<IServiceResult> UpdateStatistics(RankPost[] dto)
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
				foreach (var item in dto)
				{
					stat = await _daoStat.GetById(item.Id);
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
			}
			if (response.ValidationResult.IsValid)
			{
				response.Value = _repoChamp.UpdateStatistics(list.ToArray());
				await _uow.Commit();
			}
			return response;
		}

		public async Task<TeamStatisticDTO[]> TeamStats(string id)
		{
			TeamStatisticDTO[] result;
			result = _mapper.Map<TeamStatisticDTO[]>(await _daoTeamSubscribe
				.GetByChampionshipTeamStatistics(id));
			return result;
		}

		public async Task<PlayerStatisticDTO[]> PlayerStats(string id)
		{
			PlayerStatisticDTO[] result;
			result = _mapper.Map<PlayerStatisticDTO[]>(await _daoPlayerSubscribe
				.GetByChampionshipPlayerStatistics(id));
			return result;
		}
	}
}
