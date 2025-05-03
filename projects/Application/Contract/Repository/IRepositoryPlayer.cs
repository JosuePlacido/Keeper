
using System.Threading.Tasks;
using Domain.Core;
using Domain.Models;
namespace Application.Contract.Repository
{
	public interface IRepositoryPlayer : IRepositoryBase<Player>
	{
		Task<Player[]> GetAvailables(string terms, string championship, int page, int take);
	}
}
