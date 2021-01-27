using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Application.Interface;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Application.Validation;
using Keeper.Application.Models;
using FluentValidation.Results;
using Keeper.Application.DAO;
using Domain.Core;

namespace Keeper.Application.Services
{
	public class TeamService : ITeamService
	{
		private readonly IRepositoryTeam _repo;
		private readonly IMapper _mapper;
		private readonly IDAOTeam _dao;
		private readonly IUnitOfWork _uow;
		public TeamService(IMapper mapper, IUnitOfWork uow, IRepositoryTeam repoChamp, IDAOTeam dao)
		{
			_mapper = mapper;
			_repo = repoChamp;
			_dao = dao;
			_uow = uow;
		}
		public async Task<Team> Create(TeamCreateDTO dto)
		{
			Team Team = _mapper.Map<Team>(dto);
			Team result = await _repo.Add(Team);
			await _uow.Commit();
			return result;
		}

		public async Task<IServiceResult> Delete(string id)
		{
			ServiceResponse response = new ServiceResponse();
			TeamViewDTO dto = (TeamViewDTO)await _dao.GetByIdView(id);

			response.ValidationResult = new TeamDeleteValidation().Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				Team team = _mapper.Map<Team>(dto);
				response.Value = await _repo.Remove(team);
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
			return await _repo.GetById(id);
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
				Total = await _dao.GetTotalFromSearch(terms, notInChampinship),
				Teams = await _repo.GetAllAvailableForChampionship(terms, notInChampinship, page, take)
			};
		}

		public async Task<Team[]> List()
		{
			return await _repo.GetAll();
		}

		public async Task<IServiceResult> Update(TeamUpdateDTO dto)
		{
			ServiceResponse response = new ServiceResponse();

			response.ValidationResult = new ValidationResult();
			if (await _repo.GetById(dto.Id) != null)
			{
				Team team = _mapper.Map<Team>(dto);
				response.Value = await _repo.Update(team);
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
