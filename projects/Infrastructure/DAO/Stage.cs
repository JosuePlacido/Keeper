using Keeper.Domain.Models;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;

namespace Keeper.Infrastruture.DAO
{
	public class DAOStage : DAOBase<Stage>
	{
		public DAOStage(ApplicationContext Context) : base(Context) { }
	}
}
