using System;
using System.Threading.Tasks;
using Keeper.Application.DAO;

namespace Keeper.Domain.Core
{
	public interface IUnitOfWork
	{
		Task Commit();
		object GetDAO(Type type);
	}
}
