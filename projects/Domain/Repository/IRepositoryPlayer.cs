
using System.Threading.Tasks;
using Keeper.Domain.Core;
using Keeper.Domain.Models;
namespace Keeper.Domain.Repository
{
	public interface IRepositoryPlayer : IRepositoryBase<Player>
	{
		Task<Player[]> GetAvailables(string terms, string championship, int page, int take);
	}
}
