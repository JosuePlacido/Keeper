using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastructure.DAO
{
	public class DAOEventGame : DAOBase<EventGame>
	{
		public DAOEventGame(ApplicationContext Context) : base(Context){}
	}
}
