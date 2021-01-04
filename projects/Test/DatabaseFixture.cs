using Domain.Models;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Test
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
            var context = new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
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
												
                        context.Set<Championship>().Add(SeedData.GetChampionship());

                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}