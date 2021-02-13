using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Keeper.Application.Contract;
using Keeper.Application.DTO;
using Keeper.Domain.Models;
using Keeper.Domain.Repository;

namespace Keeper.Application.Services.RegisterResult
{
	public class RegisterResultService : IRegisterResultService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		private readonly IRepositoryMatch _repo;
		public RegisterResultService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
			_repo = ((IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch)));
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Match> GetMatch(string id)
		{
			var test = await _repo.GetByIdWithTeamsAndPlayers(id);
			return await _repo.GetByIdWithTeamsAndPlayers(id);
		}

		public async Task<IServiceResponse> RegisterResult(MatchResultDTO dto)
		{
			IServiceResponse response = new ServiceResponse();
			response.ValidationResult = new RegisterResultValidation().Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				Match match = await _repo.GetByIdWithTeamsAndPlayers(dto.Id);
				match.RegisterResult(dto.GoalsHome, dto.GoalsAway, dto.GoalsPenaltyHome, dto.GoalsPenaltyAway,
					dto.Events.Select((ev, index) => new EventGame(index, ev.Description
						, ev.Type, ev.IsHomeEvent, ev.MatchId, ev.PlayerId)).ToArray());

				if (match.AggregateGoalsHome == match.AggregateGoalsAway && match.FinalGame
					&& match.Penalty)
				{
					if (match.GoalsPenaltyHome == null)
					{
						response.ValidationResult.Errors.Add(new ValidationFailure("GoalsPenaltyHome",
							"Gols em penaltis do Mandante é obrigatório neste caso"));
					}
					if (match.GoalsPenaltyAway == null)
					{
						response.ValidationResult.Errors.Add(new ValidationFailure("GoalsPenaltyAway",
							"Gols em penaltis do Visitante é obrigatório neste caso"));
					}
				}
				if (response.ValidationResult.IsValid)
				{
					response.Value = await _repo.RegisterResult(match);
					// Registrar domain event
					await _uow.Commit();
				}
			}
			return response;
		}
	}
}
