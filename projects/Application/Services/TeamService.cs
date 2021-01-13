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
using Infrastructure.CrossCutting.DTO;

namespace Application.Services
{
	public class TeamService : ITeamService
	{
		private readonly IRepositoryTeam _repo;
		private readonly IMapper _mapper;
		public TeamService(IMapper mapper, IRepositoryTeam repoChamp)
		{
			_mapper = mapper;
			_repo = repoChamp;
		}
		public async Task<Team> Create(TeamCreateDTO dto)
		{
			Team Team = _mapper.Map<Team>(dto);
			return await _repo.Add(Team);
		}

		public async Task<Team> Delete(Team dto)
		{
			return await _repo.Remove(dto);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public async Task<Team> Get(string id)
		{
			return await _repo.GetById(id);
		}

		public async Task<Team[]> List()
		{
			return await _repo.GetAll();
		}

		public async Task<Team> Update(TeamUpdateDTO dto)
		{
			Team Team = _mapper.Map<Team>(dto);
			return await _repo.Update(Team);
		}
	}
}
