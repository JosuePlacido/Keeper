using System.Threading.Tasks;

namespace Domain.Core
{
	public interface IUnitOfWork
	{
		Task Commit();
	}
}
