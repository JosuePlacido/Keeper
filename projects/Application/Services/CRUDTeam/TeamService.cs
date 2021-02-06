using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Application.Contract;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Application.Validation;
using FluentValidation.Results;
using Keeper.Domain.Core;

namespace Keeper.Application.Services
{
	public class TeamService : ITeamService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;
		public TeamService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
		}
		public async Task<Team> Create(TeamCreateDTO dto)
		{
			Team Team = _mapper.Map<Team>(dto);
			Team result = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).Add(Team);
			await _uow.Commit();
			return result;
		}

		public async Task<IServiceResponse> Delete(string id)
		{
			ServiceResponse response = new ServiceResponse();
			TeamViewDTO dto = (TeamViewDTO)await ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam))).GetByIdView(id);

			response.ValidationResult = new TeamDeleteValidation().Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				Team team = _mapper.Map<Team>(dto);
				response.Value = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).Remove(team);
				await _uow.Commit();
			}
			return response;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Team> Get(string id)
		{
			return await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetById(id);
		}

		public async Task<TeamPaginationDTO> GetTeamsAvailablesForChampionship(string terms,
			 string notInChampinship, int page, int take)
		{
			return new TeamPaginationDTO
			{
				NotInChampionship = notInChampinship,
				Page = page,
				Take = take,
				Terms = terms,
				Total = await ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam))).GetTotalFromSearch(terms, notInChampinship),
				Teams = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetAllAvailableForChampionship(terms, notInChampinship, page, take)
			};
		}

		public async Task<Team[]> List()
		{
			return await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetAll();
		}

		public async Task<IServiceResponse> Update(TeamUpdateDTO dto)
		{
			ServiceResponse response = new ServiceResponse();

			response.ValidationResult = new ValidationResult();
			if (await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetById(dto.Id) != null)
			{
				Team team = _mapper.Map<Team>(dto);
				response.Value = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).Update(team);
				await _uow.Commit();
			}
			else
			{
				response.ValidationResult.Errors.Add(new ValidationFailure("Id"
					, "Time não encontrado"));
			}
			return response;
		}
	}
}
