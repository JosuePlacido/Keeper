using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public sealed class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions options) : base(options)
		{
			this.ChangeTracker.LazyLoadingEnabled = false;
		}

		public DbSet<Championship> Championships { get; set; }
		public DbSet<Category> Categorys { get; set; }
		public DbSet<Stage> Stages { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Match> Matchs { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<TeamSubscribe> TeamSubscribes { get; set; }
		public DbSet<PlayerSubscribe> PlayerSubscribe { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<EventGame> EventGames { get; set; }
		public DbSet<Statistic> Statistics { get; set; }
		public DbSet<Vacancy> Vacancys { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if (modelBuilder is null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
		}
	}
}
