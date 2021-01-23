using Keeper.Infrastructure.DAO;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOGroup : DAOBase<Group>
	{
		public DAOGroup(ApplicationContext Context) : base(Context) { }
	}
}
