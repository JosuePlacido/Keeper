using AutoMapper;
using System;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Application.Interface;
using Keeper.Infrastructure.CrossCutting.DTO;
using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Application.Validation;
using Keeper.Application.Models;
using FluentValidation.Results;

namespace Keeper.Application.Services
{
	public class PlayerService : IPlayerService
	{
		private readonly IRepositoryPlayer _repo;
		private readonly IMapper _mapper;
		private readonly IDAOPlayer _dao;
		public PlayerService(IMapper mapper, IRepositoryPlayer repoChamp, IDAOPlayer dao)
		{
			_mapper = mapper;
			_repo = repoChamp;
			_dao = dao;
		}
		public async Task<Player> Create(PlayerCreateDTO dto)
		{
			Player Player = _mapper.Map<Player>(dto);
			return await _repo.Add(Player);
		}

		public async Task<IServiceResult> Delete(string id)
		{
			ServiceResponse response = new ServiceResponse();
			PlayerViewDTO dto = await _dao.GetByIdView(id);

			response.ValidationResult = new PlayerDeleteValidation().Validate(dto);
			if (response.ValidationResult.IsValid)
			{
				Player player = _mapper.Map<Player>(dto);
				response.Value = await _repo.Remove(player);
			}
			return response;
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Player> Get(string id)
		{
			return await _repo.GetById(id);
		}

		public async Task<PlayerPaginationDTO> GetAvailables(string terms = "", string championship = "",
			int page = 1, int take = 10)
		{
			return new PlayerPaginationDTO
			{
				ExcludeFromChampionship = championship,
				Take = take,
				Page = page,
				Terms = terms,
				Players = await _repo.GetAvailables(terms, championship, page, take),
				Total = await _dao.GetTotalFromSearch(terms, championship)
			};
		}

		public async Task<IServiceResult> Update(PlayerUpdateDTO dto)
		{
			ServiceResponse response = new ServiceResponse();

			response.ValidationResult = new ValidationResult();
			if (await _repo.GetById(dto.Id) != null)
			{
				Player player = _mapper.Map<Player>(dto);
				response.Value = await _repo.Update(player);
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
