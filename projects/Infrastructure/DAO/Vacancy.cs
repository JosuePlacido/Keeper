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
	}
}
