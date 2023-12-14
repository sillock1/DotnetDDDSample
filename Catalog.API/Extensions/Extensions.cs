using System.Reflection;
using MassTransit;

namespace Catalog.API.Extensions;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CatalogContext>("CatalogDB",
            configureDbContextOptions: dbContextOptionsBuilder =>
            {
                dbContextOptionsBuilder.UseNpgsql(builder => { });
            });

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigration<CatalogContext, CatalogContextSeed>();
        }
        // Add the integration services that consume the DbContext

        builder.Services.AddMassTransit(x =>
        {
            if (builder.Environment.IsDevelopment())
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("AzureServiceBus");
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.UsingAzureServiceBus(((context, cfg) =>
                {
                    cfg.Host(connectionString);
                    cfg.ConfigureEndpoints(context);
                }));
            }
        });
    }
}