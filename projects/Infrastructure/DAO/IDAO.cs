using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DAO
{
	public interface IDAO<TEntity> where TEntity : class
	{
		void Add(TEntity obj);

		Task<TEntity> GetById(string id);

		Task<TEntity[]> GetAll();

		void Update(TEntity obj);

		void Remove(TEntity obj);

		void Dispose();
	}
}
