using System;
using System.Threading.Tasks;

namespace Keeper.Application.Contract
{
	public interface IUnitOfWork
	{
		Task Commit();
		object GetDAO(Type type);
	}
}
