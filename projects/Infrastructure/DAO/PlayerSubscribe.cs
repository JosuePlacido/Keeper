using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOPlayerSubscribe : DAOBase<PlayerSubscribe>
	{
		public DAOPlayerSubscribe(ApplicationContext Context) : base(Context) { }
	}
}
