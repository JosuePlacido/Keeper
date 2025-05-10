using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Application.Contract.DAL;
using System;
using Domain.Utils;
using System.Collections.Generic;
using Application.DTO;

namespace Infrastructure.DAO;
public class DAOTeam : DAO, IDAOTeam
{
	public DAOTeam(ApplicationContext Context) : base(Context) { }

	public async Task<string[]> Exists(string[] ids)
	{
		List<string> idNotFound = new();
		Team team;
		foreach (var id in ids)
		{
			team = await _context.Teams.AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (team == null)
			{
				idNotFound.Add(id);
			}
		}
		return idNotFound.ToArray();
	}

	public async Task<(Team team, bool isDeletable)> GetByIdDeletable(string id)
	{
		Team team = await _context.Teams.AsNoTracking()
			.Where(t => t.Id == id).FirstOrDefaultAsync();
		bool isDeletable = team != null &&
			!await _context.TeamSubscribes.Where(ts => ts.TeamId == id).AnyAsync();
		return (team, isDeletable);
	}

	public async Task<PaginationDTO<Team>> GetsNotInChampionship(string terms, string championshipId, int page, int take)
	{
		string termsNormalized = StringUtils.NormalizeLower(terms);
		string termsNormalizedUpper = termsNormalized.ToUpper();

		string[] subscribed = await _context.TeamSubscribes.AsNoTracking()
			.Where(ts => ts.ChampionshipId == championshipId)
			.Select(ts => ts.TeamId)
			.ToArrayAsync();

		IQueryable<Team> query = _context.Teams.AsNoTracking()
			.Where(t => !subscribed.Contains(t.Id))
			.Where(t => EF.Property<string>(t, "NormalizedName").Contains(termsNormalized) ||
				t.Abrev.Contains(termsNormalizedUpper));
		int total = await query.CountAsync();
		Team[] teams = await query
			.OrderBy(t => t.Name)
			.Skip((page - 1) * take).Take(take).ToArrayAsync();
		return new PaginationDTO<Team>
		{
			Take = take,
			Page = page,
			Items = teams,
			Total = total
		};
	}

	public async Task<PaginationDTO<Team>> List(string terms, int page, int take)
	{
		string termsNormalized = StringUtils.NormalizeLower(terms);
		string termsNormalizedUpper = termsNormalized.ToUpper();

		IQueryable<Team> query = _context.Teams.AsNoTracking()
			.Where(t => EF.Property<string>(t, "NormalizedName").Contains(termsNormalized) ||
				t.Abrev.Contains(termsNormalizedUpper));
		int total = await query.CountAsync();
		Team[] teams = await query
			.OrderBy(t => t.Name)
			.Skip((page - 1) * take).Take(take).ToArrayAsync();
		return new PaginationDTO<Team>
		{
			Take = take,
			Page = page,
			Items = teams,
			Total = total
		};
	}
}