using Domain.Models;
using Infrastructure.Data;
using Application.Contract.Repository;

namespace Infrastructure.Repository;
public class TeamRepository : RepositoryBase<Team>, IRepositoryTeam
{
	public TeamRepository(ApplicationContext Context) : base(Context) { }
}
