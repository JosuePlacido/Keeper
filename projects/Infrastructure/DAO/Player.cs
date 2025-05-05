using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contract.DAL;
using System.Collections.Generic;
using Application.DTO;

namespace Infrastructure.DAO;
public class DAOPlayer : DAO, IDAOPlayer
{
	public DAOPlayer(ApplicationContext Context) : base(Context) { }

	public async Task<string[]> Exists(string[] ids)
	{
		List<string> idNotFound = new List<string>();
		Player player;
		foreach (var id in ids)
		{
			player = await _context.Players.AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
			if (player == null)
			{
				idNotFound.Add(id);
			}
		}
		return idNotFound.ToArray();
	}

	public async Task<PaginationDTO<Player>> GetAvailables(string terms, string championship, int page, int take)
	{
		string[] playersSubscribedId = _context.PlayerSubscribe.AsNoTracking()
			.Where(ps => ps.ChampionshipId == championship && !ps.IsFreeAgent)
			.Select(ts => ts.PlayerId).ToArray();

		IQueryable<Player> query = _context.Players.AsNoTracking()
			.Where(p => !playersSubscribedId.Contains(p.Id))
			.Where(p => EF.Property<string>(p, "NormalizedName").Contains(terms)
				|| EF.Property<string>(p, "NormalizedNick").Contains(terms));
		int total = await query.CountAsync();
		Player[] playersAvailable = await query.OrderBy(t => EF.Property<string>(t, "NormalizedName"))
			.Skip((page - 1) * take)
			.Take(take).ToArrayAsync();
		return new PaginationDTO<Player>
		{
			Take = take,
			Page = page,
			Items = playersAvailable,
			Total = total
		};
	}

	public async Task<bool> IsDeletable(string id)
	{
		var player = await _context.Players.AsNoTracking()
			.Where(t => t.Id == id).FirstOrDefaultAsync();
		return player != null &&
			_context.PlayerSubscribe.Where(ts => ts.PlayerId == id).Count() == 0;
	}

	public async Task<PaginationDTO<Player>> List(string terms, int page, int take)
	{
		IQueryable<Player> query = _context.Players.AsNoTracking()
			.Where(p => EF.Property<string>(p, "NormalizedName").Contains(terms)
				|| EF.Property<string>(p, "NormalizedNick").Contains(terms));
		int total = await query.CountAsync();
		Player[] playersAvailable = await query.OrderBy(t => EF.Property<string>(t, "NormalizedName"))
			.Skip((page - 1) * take)
			.Take(take).ToArrayAsync();
		return new PaginationDTO<Player>
		{
			Take = take,
			Page = page,
			Items = playersAvailable,
			Total = total
		};
	}

}
