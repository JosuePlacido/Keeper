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
using System.Collections.Generic;

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
		public async Task<MatchEditsScope> Create(ChampionshipCreateDTO dto)
		{
			var championship = _mapper.Map<Championship>(dto);
			championship = await UpdatadeReferences(championship, dto);
			foreach (var stage in championship.Stages)
			{
				foreach (var group in stage.Groups)
				{
					group.RoundRobinMatches(stage.DuplicateTurn, stage.MirrorTurn);
				}
			}
			_repoChamp.Add(championship);
			return _mapper.Map<MatchEditsScope>(championship);
		}
		public async Task<MatchEditsScope> CheckMatches(MatchEditsScope dto)
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
		private async Task<Championship> UpdatadeReferences(Championship championship, ChampionshipCreateDTO dto)
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
	}
}
