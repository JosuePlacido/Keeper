using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.DAO
{
	public class DAOPlayerSubscribe : DAOBase<PlayerSubscribe>
	{
		private readonly ApplicationContext _context;
		public DAOPlayerSubscribe(ApplicationContext Context)
			: base(Context) => _context = Context;

		public string[] VerifyId(string[] players) => _context.PlayerSubscribe.Where(player => players.Contains(player.Id))
				.Select(player => player.Id).ToArray();
	}
}
