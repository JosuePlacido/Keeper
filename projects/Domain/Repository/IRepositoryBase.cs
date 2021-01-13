using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Repository
{
	public interface IRepositoryBase<TEntity> where TEntity : class
	{
		Task<TEntity> Add(TEntity obj);

		Task<TEntity> GetById(string id);

		Task<TEntity[]> GetAll();

		Task<TEntity> Update(TEntity obj);

		Task<TEntity> Remove(TEntity obj);

		void Dispose();
	}
}
