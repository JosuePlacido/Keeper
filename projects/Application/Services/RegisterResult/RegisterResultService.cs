using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Application.Contract.DAL;
using Application.DTO;
using Domain.Models;
using Application.Contract.Repository;
using Application.Contract;

namespace Application.Services.RegisterResult
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
			_repo = (IRepositoryMatch)_uow.GetDAO(typeof(IRepositoryMatch));
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Match> GetMatch(string id)
		{
			return await _repo.GetByIdWithTeamsAndPlayers(id);
		}

		public async Task<IServiceResponse> RegisterResult(MatchResultDTO dto)
		{
			IServiceResponse response = new ServiceResponse();
			Match match = await _repo.GetByIdWithTeamsAndPlayers(dto.Id);
			response.ValidationResult = new RegisterResultValidation(match).Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				match.RegisterResult(dto.GoalsHome, dto.GoalsAway, dto.GoalsPenaltyHome, dto.GoalsPenaltyAway,
					dto.Events.Select((ev, index) => new EventGame(index, ev.Description
						, ev.Type, ev.IsHomeEvent, ev.MatchId, ev.PlayerId)).ToArray());
				{
					response.Value = await _repo.RegisterResult(match);
					await _uow.Commit();
				}
			}
			return response;
		}
	}
}
