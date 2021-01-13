using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DAO
{
	public interface IDAO<TEntity> where TEntity : class
	{
		Task<TEntity> Add(TEntity obj);

		Task<TEntity> GetById(string id);

		Task<TEntity[]> GetAll();

		Task<TEntity> Update(TEntity obj);

		Task<TEntity> Remove(TEntity obj);

		void Dispose();
	}
}
