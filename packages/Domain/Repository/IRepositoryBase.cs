using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repository
{
	public interface IRepositoryBase<TEntity> where TEntity : class
	{
		void Add(TEntity obj);

		TEntity GetById(int id);

		IEnumerable<TEntity> GetAll();

		void Update(TEntity obj);

		void Remove(TEntity obj);

		void Dispose();
	}
}
