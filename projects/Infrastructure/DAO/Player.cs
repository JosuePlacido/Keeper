using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Infrastructure.CrossCutting.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Infrastructure.DAO
{
	public class DAOPlayer : DAOBase<Player>, IDAOPlayer
	{
		public DAOPlayer(ApplicationContext Context) : base(Context) { }

		public async Task<PlayerViewDTO> GetByIdView(string id)
		{
			var player = await _context.Players.AsNoTracking()
				.Where(t => t.Id == id).FirstOrDefaultAsync();
			return new PlayerViewDTO
			{
				Id = player?.Id,
				Name = player?.Name,
				Nickname = player?.Nickname,
				IsDeletable = _context.PlayerSubscribe.Where(ts => ts.PlayerId == id).Count() == 0
			};
		}
	}
}
