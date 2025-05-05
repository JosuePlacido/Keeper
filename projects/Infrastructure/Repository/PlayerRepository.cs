using System;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Utils;
using Application.Contract.Repository;

namespace Infrastructure.Repository;
public class PlayerRepository : RepositoryBase<Player>, IRepositoryPlayer
{
	public PlayerRepository(ApplicationContext Context) : base(Context) { }

}

