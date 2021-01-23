
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Keeper.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Linq;
using Keeper.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Keeper.Test.Integration.Api
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

				services.AddDbContext<ApplicationContext>(options =>
				{
					options.UseSqlServer("Data Source=DESKTOP-LCEM2JV;Initial Catalog=bd_keeper;Integrated Security=True");
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
						db.Database.ExecuteSqlRaw("DELETE FROM tb_team WHERE Id LIKE 'test'");
						db.Database.ExecuteSqlRaw("DELETE FROM tb_team WHERE Id LIKE 'remove'");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_team(Id,Name) VALUES ('test','test')");
						db.Database.ExecuteSqlRaw("INSERT INTO tb_team(Id,Name) VALUES ('remove','remove')");
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
			(context, config) =>
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
