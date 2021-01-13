using System;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.DTO;
using Domain.Models;
using FluentValidation.Results;

namespace Application.Interface
{
	public interface IChampionshipService : IDisposable
	{
		Task<MatchEditsScope> Create(ChampionshipCreateDTO dto);
		Task<MatchEditsScope> CheckMatches(MatchEditsScope dto);
	}
}
