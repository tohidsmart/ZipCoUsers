using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserManaging.API;
using UserManaging.Infrastructure;

namespace UserManaging.Tests
{
    public class UserManagingApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public UserManagingApplicationFactory()
        {
            connection.Open();
        }
        private readonly SqliteConnection connection
          = new SqliteConnection($"DataSource=:memory:");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();


                services.SetupTestDatabase<UserManagingContext>(connection);

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<UserManagingContext>();

                var logger = scopedServices.GetRequiredService<ILogger<UserManagingApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                appDb.Database.EnsureCreated();
            });
        }

    
    }
}
