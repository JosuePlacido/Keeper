using Infrastructure.DAO;
using Infrastructure.Data;
using Domain.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.DAO
{
	public class DAOVacancy : DAOBase<Vacancy>
	{
		private readonly ApplicationContext _context;
		public DAOVacancy(ApplicationContext Context)
			: base(Context) => _context = Context;

		public string[] VerifyId(string[] vacancys) => _context.Vacancys.Where(vacancy => vacancys.Contains(vacancy.Id))
				.Select(vacancy => vacancy.Id).ToArray();
	}
}
