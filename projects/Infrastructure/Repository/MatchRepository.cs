using System;
using System.Reflection;
using System.Threading.Tasks;
using Keeper.Domain.Repository;
using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Keeper.Domain.Utils;

namespace Keeper.Infrastructure.Repository
{
	public class MatchRepository : RepositoryBase<Match>, IRepositoryMatch
	{
		public MatchRepository(ApplicationContext Context) : base(Context) { }
	}
}
