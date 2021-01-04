using Domain.Repository;
using Domain.Models;
using Infrastructure.Data;
namespace Infrastruture.Repositorys
{
	public class RepositoryVacancy : RepositoryBase<Vacancy>, IRepositoryVacancy
	{
		private readonly ApplicationContext _context;
		public RepositoryVacancy(ApplicationContext Context)
			: base(Context)
		{
			_context = Context;
		}
	}
}
