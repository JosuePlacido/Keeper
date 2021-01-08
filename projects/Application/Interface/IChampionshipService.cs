using System;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;
using FluentValidation.Results;

namespace Application.Interface{
    public interface IChampionshipService: IDisposable
    {
        Championship Create(ChampionshipCreateDTO dto);
    }
}
