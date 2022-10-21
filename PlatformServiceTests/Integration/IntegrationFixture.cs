using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PlatformService.Data;

namespace PlatformServiceTests.Integration;

public class IntegrationFixture: IDisposable, ICollectionFixture<IntegrationFixture>
{
    private WebApplicationFactory<PlatformService.Program> _application;
    public HttpClient Client { get; }

    public IntegrationFixture()
    {
        _application = new WebApplicationFactory<PlatformService.Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                // var logger = scopedServices
                //     .GetRequiredService<ILogger<IntegrationFixture>>();

                db.Database.EnsureCreated();

                // try
                // {
                //     Utilities.InitializeDbForTests(db);
                // }
                // catch (Exception ex)
                // {
                //     logger.LogError(ex, "An error occurred seeding the " +
                //                         "database with test messages. Error: {Message}", ex.Message);
                // }
            });
        });
        Client = _application.CreateClient();
    }
    public void Dispose()
    {
        Client.Dispose();
        _application.Dispose();
    }
}