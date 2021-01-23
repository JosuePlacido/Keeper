using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastruture.DAO
{
	public class DAOStatistics : DAOBase<Statistic>
	{
		public DAOStatistics(ApplicationContext Context) : base(Context) { }
	}
}
