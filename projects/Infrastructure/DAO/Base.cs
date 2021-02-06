using System.Linq;
using System.Threading.Tasks;
using Keeper.Domain.Models;
using Keeper.Application.DTO;
using Keeper.Infrastructure.DAO;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Keeper.Application.Contract;
using Keeper.Domain.Utils;
using System.Collections.Generic;
using Keeper.Domain.Enum;
using Keeper.Domain.Core;

namespace Keeper.Infrastructure.DAO
{
	public abstract class DAO : IDAO
	{
		protected readonly ApplicationContext _context;
		public DAO(ApplicationContext Context)
		{
			_context = Context;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
