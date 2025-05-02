using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Keeper.Test;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Data.Common;
using System.Linq;

namespace Keeper.Test
{
	public class SharedDatabaseFixture : IDisposable
	{
		public DbConnection Connection { get; }
		public SharedDatabaseFixture()
		{
			Connection = new SqliteConnection("Filename=:memory:");
			Connection.Open();
			var context = CreateContext();
			context.Database.EnsureCreated();
			Seed(context);
		}

		public ApplicationContext CreateContext(DbTransaction transaction = null)
		{
			var context = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
				.UseSqlite(Connection).Options);

			if (transaction != null)
			{
				context.Database.UseTransaction(transaction);
			}

			return context;
		}

		private void Seed(ApplicationContext context)
		{
			context.Set<Team>().AddRange(SeedData.Teams);
			context.Set<Player>().AddRange(SeedData.Players);
			context.Set<PlayerSubscribe>().AddRange(SeedData.PlayersSubscribe);
			context.Set<Category>().AddRange(SeedData.Categorys);
			context.Set<Championship>().Add(SeedData.Championship);
			context.Set<TeamSubscribe>().AddRange(SeedData.TeamsSubscribes);
			context.Set<Stage>().AddRange(SeedData.Stages);
			context.Set<Group>().AddRange(SeedData.Groups);
			context.Set<Vacancy>().AddRange(SeedData.Vacancys);
			context.Set<Statistic>().AddRange(SeedData.Statistics);
			context.Set<Match>().AddRange(SeedData.Matches);
			context.Set<EventGame>().AddRange(SeedData.EventGames);
			context.Set<Championship>().Add(Championship.Factory("remove",
				"remove", "remove"));
			context.SaveChanges();
		}

		public void Dispose() => Connection.Dispose();
	}
}
