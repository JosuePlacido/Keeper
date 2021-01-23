using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOTeamSubscribe : DAOBase<TeamSubscribe>
	{
		public DAOTeamSubscribe(ApplicationContext Context) : base(Context) { }

	}
}
