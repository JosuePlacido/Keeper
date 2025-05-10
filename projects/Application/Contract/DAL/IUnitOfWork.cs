using System;
using System.Threading.Tasks;

namespace Application.Contract.DAL
{
	public interface IUnitOfWork
	{
		Task Commit();
		object GetDAO(Type type);
	}
}
