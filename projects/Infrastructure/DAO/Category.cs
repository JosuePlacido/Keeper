using Infrastructure.Data;
using Domain.Models;

namespace Infrastructure.DAO
{
	public class DAOCategory : DAOBase<Category>
	{
		private readonly ApplicationContext _context;
		public DAOCategory(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
