using AutoMapper;
using System;
using System.Threading.Tasks;
using Application.Contract.DAL;
using Domain.Models;
using Application.DTO;
using FluentValidation.Results;
using Application.Contract.Repository;

namespace Application.Services.CRUDTeam;
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

	public async Task<Team> Delete(string id)
	{
		var (team, isDeletable) = await ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam))).GetByIdDeletable(id);

		if (team != null && isDeletable)
		{
			team = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam)))
				.Remove(team);
			await _uow.Commit();
			return team;
		}

		throw new ValidationException("Falha ao excluir time",
			new ValidationFailure[1] {
				new("Id", team != null?"Time inscrito em campeonato":"Time não encontrado")
			});
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	public async Task<Team> Get(string id)
	{
		return await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetById(id);
	}

	public async Task<PaginationDTO<Team>> GetTeamsAvailablesForChampionship(
		string championshipID, string terms, int page, int take)
	=> await ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam)))
				.GetsNotInChampionship(terms, championshipID, page, take);

	public async Task<PaginationDTO<Team>> List(string terms, int page, int take) =>
		await ((IDAOTeam)_uow.GetDAO(typeof(IDAOTeam))).List(terms, page, take);

	public async Task<Team> Update(TeamUpdateDTO dto)
	{
		if (await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).GetById(dto.Id) != null)
		{
			Team team = _mapper.Map<Team>(dto);
			team = await ((IRepositoryTeam)_uow.GetDAO(typeof(IRepositoryTeam))).Update(team);
			await _uow.Commit();
			return team;
		}
		throw new ValidationException("Falha ao alterar time",
			new ValidationFailure[1] { new("Id", "Time não encontrado") });
	}
}