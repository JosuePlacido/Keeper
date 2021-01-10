using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.DAO
{
	public class DAOTeamSubscribe : DAOBase<TeamSubscribe>
	{
		private readonly ApplicationContext _context;
		public DAOTeamSubscribe(ApplicationContext Context)
			: base(Context) => _context = Context;

		public string[] VerifyId(string[] teams) => _context.TeamSubscribes.Where(team => teams.Contains(team.Id))
				.Select(team => team.Id).ToArray();
	}
}
