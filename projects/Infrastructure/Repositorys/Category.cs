using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryCategory : RepositoryBase<Category>, IRepositoryCategory
	{
		private readonly ApplicationContext _context;
		public RepositoryCategory(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
