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

namespace Keeper.Application.Services.MatchService
{
	public class MatchService : IMatchService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly IRepositoryMatch _repo;
		public MatchService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
			_repo = ((IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch)));
		}

		public MatchEditsScope CheckMatches(MatchEditsScope dto)
		{
			if (dto != null && dto.Stages != null)
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

		public async Task<MatchEditsScope> GetMatchSchedule(string id)
		{
			return _mapper.Map<MatchEditsScope>(
				await ((IRepositoryChampionship)_uow.GetDAO(typeof(IRepositoryChampionship)))
					.GetByIdWithMatchWithTeams(id));
		}

		public async Task<IServiceResponse> UpdateMatches(MatchEditedDTO[] dto)
		{
			IServiceResponse response = new ServiceResponse();
			response.ValidationResult = new ValidationResult();
			if (dto == null || dto.Length == 0)
			{
				return response;
			}
			List<Match> matches = new List<Match>();
			Match temp;
			foreach (var match in dto)
			{
				if (string.IsNullOrEmpty(match.Id))
				{
					matches.Add(await _repo.Add(new Match((int)match.Round, match.Status, match.Name,
							match.HomeId, match.AwayId, match.VacancyHomeId, match.VacancyAwayId,
							match.Date, match.Address, (bool)match.AggregateGame,
						(bool)match.FinalGame, (bool)match.Penalty)));
				}
				else
				{
					temp = await _repo.GetById(match.Id);
					if (temp != null)
					{
						temp.EditScope(match.Round, match.Status, match.Name,
							match.HomeId, match.AwayId, match.VacancyHomeId, match.VacancyAwayId,
							match.Date, match.Address, match.AggregateGame,
							match.FinalGame, match.Penalty);
						matches.Add(await _repo.Update(temp));
					}
				}
			}
			await _uow.Commit();
			response.Value = matches;
			return response;
		}
	}
}
