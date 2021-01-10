using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.DAO
{
	public class DAOGroup : DAOBase<Group>
	{
		private readonly ApplicationContext _context;
		public DAOGroup(ApplicationContext Context)
			: base(Context) => _context = Context;

		public string[] VerifyId(string[] groups) => _context.Groups.Where(group => groups.Contains(group.Id))
				.Select(group => group.Id).ToArray();
	}
}
