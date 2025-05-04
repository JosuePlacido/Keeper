
using Microsoft.Extensions.Configuration;
using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Domain.Models;
using System.IO;

namespace Test.EndToEnd
{
	public sealed class CustomWebApplicationFactory : WebApplicationFactory<Startup>
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType ==
						typeof(DbContextOptions<ApplicationContext>));

				services.Remove(descriptor);
				IConfigurationRoot configuration = new ConfigurationBuilder()
					.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
					.AddJsonFile("appsettings.Test.json", false)
					.AddEnvironmentVariables()
					.Build();

				string connectionString = configuration.GetValue<string>("PersistenceModule:DefaultConnection");

				services.AddDbContext<ApplicationContext>(options =>
				{
					options.UseSqlServer(connectionString);
				});

				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<ApplicationContext>();
					var logger = scopedServices
						.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

					db.Database.EnsureCreated();
					try
					{

						db.Database.ExecuteSqlRaw("DELETE FROM tb_championship WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_team_subscribe WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_player_subscribe WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_team WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_team WHERE Id LIKE 'remove'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_player WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_player WHERE Id LIKE 'remove'");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_team(Id,Name) VALUES ('test','test')");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_team(Id,Name) VALUES ('remove','remove')");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_player(Id,Name) VALUES ('test','test')");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_player(Id,Name) VALUES ('remove','remove')");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_championship(Id,Name,Edition) VALUES ('test','test','test')");
						db.Set<TeamSubscribe>().Add(TeamSubscribe.Factory("test", "test", "test"));
						db.Set<PlayerSubscribe>().Add(PlayerSubscribe.Factory("test",
							"test", "test"));
						db.SaveChanges();
					}
					catch (Exception ex)
					{
						logger.LogError(ex, "An error occurred seeding the " +
							"database with test messages. Error: {Message}", ex.Message);
					}
				}
			});
		}

		/*
		=> builder.ConfigureAppConfiguration(
			(db, config) =>
			{
				config.AddInMemoryCollection(
					new Dictionary<string, string>
					{
						["FeatureManagement:SQLServer"] = "true",
						["PersistenceModule:DefaultConnection"] =
							"Data Source=DESKTOP-LCEM2JV;Initial Catalog=bd_keeper;Integrated Security=True",

					});
				config.ad
			});*/
	}
}
