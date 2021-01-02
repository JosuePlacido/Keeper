using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public sealed class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options) { }

		public DbSet<Team> Teams { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if (modelBuilder is null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
			//SeedData.Seed(modelBuilder);
		}
	}
}
