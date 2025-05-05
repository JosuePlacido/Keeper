using AutoMapper;
using System;
using System.Threading.Tasks;
using Application.Contract.DAL;
using Domain.Models;
using FluentValidation.Results;
using Application.DTO;
using Application.Contract.Repository;

namespace Application.Services.CRUDPlayer;
public class PlayerService : IPlayerService
{
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;
	public PlayerService(IMapper mapper, IUnitOfWork uow)
	{
		_mapper = mapper;
		_uow = uow;
	}
	public async Task<Player> Create(PlayerCreateDTO dto)
	{
		Player Player = _mapper.Map<Player>(dto);
		var result = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer)))
			.Add(Player);
		await _uow.Commit();
		return result;
	}

	public async Task<Player> Delete(string id)
	{
		Player player = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer)))
			.GetById(id) ?? throw new ValidationException("Falha ao excluir jogador",
				new ValidationFailure[1] { new("Id", "Jogador não encontrado") });

		if (await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer))).IsDeletable(id))
		{
			player = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer)))
				.Remove(player);
			await _uow.Commit();
			return player;
		}
		throw new ValidationException("Não é possivel excluir jogador",
			new ValidationFailure[1] { new("Id", "Jogador inscrito em campeonato") });
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}

	public async Task<Player> Get(string id)
	{
		return await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetById(id);
	}

	public async Task<PaginationDTO<Player>> List(string terms = "", int page = 1, int take = 10)
	{
		return await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer)))
			.List(terms, page, 10);
	}

	public async Task<PaginationDTO<Player>> GetAvailables(string terms = "",
		string championship = "", int page = 1, int take = 10) =>
		await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer)))
			.GetAvailables(terms, championship, page, take);


	public async Task<Player> Update(PlayerUpdateDTO dto)
	{
		if (await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetById(dto.Id) != null)
		{
			Player player = _mapper.Map<Player>(dto);
			player = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).Update(player);
			await _uow.Commit();
			return player;
		}
		throw new ValidationException("Falha ao alterar jogador",
			new ValidationFailure[1] { new("Id", "Jogador não encontrado") });
	}
}
