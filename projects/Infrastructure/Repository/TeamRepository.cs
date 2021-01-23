using System;
using System.Reflection;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Keeper.Infrastructure.Repository
{
	public class TeamRepository : RepositoryBase<Team>, IRepositoryTeam
	{
		public TeamRepository(ApplicationContext Context) : base(Context) { }
	}
}
