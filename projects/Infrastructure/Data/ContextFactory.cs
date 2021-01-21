using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Keeper.Infrastructure.Data
{
	public sealed class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
	{
		public ApplicationContext CreateDbContext(string[] args)
		{
			string connectionString = ReadDefaultConnectionStringFromAppSettings();

			DbContextOptionsBuilder<ApplicationContext> builder = new DbContextOptionsBuilder<ApplicationContext>();
			Console.WriteLine(connectionString);
			builder.UseSqlServer(connectionString);
			builder.EnableSensitiveDataLogging();
			return new ApplicationContext(builder.Options);
		}

		private static string ReadDefaultConnectionStringFromAppSettings()
		{
			string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
				.AddJsonFile("appsettings.json", false)
				.AddJsonFile($"appsettings.{envName}.json", false)
				.AddEnvironmentVariables()
				.Build();

			string connectionString = configuration.GetValue<string>("PersistenceModule:DefaultConnection");
			return connectionString;
		}
	}
}
