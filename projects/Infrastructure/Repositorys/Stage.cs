using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryStage : RepositoryBase<Stage>, IRepositoryStage
	{
		private readonly ApplicationContext _context;
		public RepositoryStage(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
