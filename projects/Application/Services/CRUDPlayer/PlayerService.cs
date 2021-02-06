using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Application.Contract;
using Keeper.Domain.Models;
using Application.Validation;
using FluentValidation.Results;
using Keeper.Application.DTO;
using Keeper.Domain.Core;
using System.Collections.Generic;
using System.Linq;
using Keeper.Domain.Enum;

namespace Keeper.Application.Services
{
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

		public async Task<IServiceResponse> Delete(string id)
		{
			ServiceResponse response = new ServiceResponse();
			PlayerViewDTO dto = (PlayerViewDTO)await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer)))
				.GetByIdView(id);

			response.ValidationResult = new PlayerDeleteValidation().Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				Player player = _mapper.Map<Player>(dto);
				response.Value = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer)))
					.Remove(player);
				await _uow.Commit();
			}
			return response;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Player> Get(string id)
		{
			return await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetById(id);
		}

		public async Task<Player[]> Get()
		{
			return await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetAll();
		}

		public async Task<PlayerAvailablePaginationDTO> GetAvailables(string terms = "", string championship = "",
			int page = 1, int take = 10)
		{
			Player[] availables = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetAvailables(terms, championship, page, take);
			List<PlayerSubscribe> freeAgents = new List<PlayerSubscribe>(
				await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer))).GetFreeAgentsInChampionship(championship));

			freeAgents.AddRange(availables.Select(p => new PlayerSubscribe(p.Id, Status.FreeAgent)));
			PlayerAvailablePaginationDTO view = new PlayerAvailablePaginationDTO
			{
				ExcludeFromChampionship = championship,
				Take = take,
				Page = page,
				Terms = terms,
				Players = freeAgents.ToArray(),
				Total = await ((IDAOPlayer)_uow.GetDAO(typeof(IDAOPlayer)))
					.GetTotalFromSearch(terms, championship)
			};
			return view;
		}

		public async Task<IServiceResponse> Update(PlayerUpdateDTO dto)
		{
			ServiceResponse response = new ServiceResponse();

			response.ValidationResult = new ValidationResult();
			if (await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).GetById(dto.Id) != null)
			{
				Player player = _mapper.Map<Player>(dto);
				response.Value = await ((IRepositoryPlayer)_uow.GetDAO(typeof(IRepositoryPlayer))).Update(player);
				await _uow.Commit();
			}
			else
			{
				response.ValidationResult.Errors.Add(new ValidationFailure("Id"
					, "Jogador não encontrado"));
			}
			return response;
		}
	}
}
