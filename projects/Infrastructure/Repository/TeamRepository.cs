using System;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
	public class TeamRepository : IRepositoryTeam
	{
		private readonly ApplicationContext _context;
		public TeamRepository(ApplicationContext Context)
		{
			_context = Context;
		}
		public async Task<Team> Add(Team obj)
		{
			await _context.Set<Team>().AddAsync(obj);
			await _context.SaveChangesAsync();
			return obj;
		}

		public async Task<Team> GetById(string id)
		{
			return await _context.Set<Team>().FindAsync(id);
		}

		public async Task<Team[]> GetAll()
		{
			return await _context.Set<Team>().AsNoTracking().ToArrayAsync();
		}

		public async Task<Team> Update(Team obj)
		{
			_context.Entry(obj).State = EntityState.Modified;
			var entry = _context.Entry(obj);
			await _context.SaveChangesAsync();
			return obj;
		}

		public async Task<Team> Remove(Team obj)
		{
			_context.Set<Team>().Remove(obj);
			await _context.SaveChangesAsync();
			return obj;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
