using Keeper.Domain.Models;
using Keeper.Infrastructure.Data;
using Keeper.Test;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace Keeper.Test
{
	public class SharedDatabaseFixture : IDisposable
	{
		private static readonly object _lock = new object();
		private static bool _databaseInitialized;

		public SharedDatabaseFixture()
		{
			Connection = new SqliteConnection("Filename=db_test.db");
			Seed();
			Connection.Open();
		}
		public DbConnection Connection { get; }

		public ApplicationContext CreateContext(DbTransaction transaction = null)
		{
			var context = new ApplicationContext(
				new DbContextOptionsBuilder<ApplicationContext>()
				.EnableSensitiveDataLogging()
				.UseSqlite(Connection).Options);

			if (transaction != null)
			{
				context.Database.UseTransaction(transaction);
			}

			return context;
		}

		private void Seed()
		{
			lock (_lock)
			{
				if (!_databaseInitialized)
				{
					using (var context = CreateContext())
					{
						context.Database.EnsureDeleted();
						context.Database.EnsureCreated();
						context.Set<Team>().AddRange(SeedData.Teams);
						context.Set<Team>().AddRange(SeedData2.Teams);
						context.Set<Player>().AddRange(SeedData.Players);
						context.Set<PlayerSubscribe>().AddRange(SeedData.PlayersSubscribe);
						context.Set<Category>().AddRange(SeedData.Categorys);
						context.Set<Championship>().Add(SeedData.Championship);
						context.Set<Championship>().Add(SeedData2.Championship);
						context.Set<TeamSubscribe>().AddRange(SeedData.TeamsSubscribes);
						context.Set<TeamSubscribe>().AddRange(SeedData2.TeamsSubscribes);
						context.Set<Stage>().AddRange(SeedData.Stages);
						context.Set<Stage>().AddRange(SeedData2.Stages);
						context.Set<Group>().AddRange(SeedData.Groups);
						context.Set<Group>().AddRange(SeedData2.Groups);
						context.Set<Vacancy>().AddRange(SeedData.Vacancys);
						context.Set<Vacancy>().AddRange(SeedData2.Vacancys);
						context.Set<Statistic>().AddRange(SeedData.Statistics);
						context.Set<Statistic>().AddRange(SeedData2.Statistics);
						context.Set<Match>().AddRange(SeedData.Matches);
						context.Set<Match>().AddRange(SeedData2.Matches);
						context.Set<EventGame>().AddRange(SeedData.EventGames);
						context.Set<Championship>().Add(Championship.Factory("remove",
							"remove", "remove"));
						context.SaveChanges();
					}

					_databaseInitialized = true;
				}
			}
		}

		public void Dispose() => Connection.Dispose();
	}
}
