using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryEventGame : RepositoryBase<EventGame>, IRepositoryEventGame
	{
		private readonly ApplicationContext _context;
		public RepositoryEventGame(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
