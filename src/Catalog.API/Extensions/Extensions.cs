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
                var connectionString = builder.Configuration.GetConnectionString("RabbitMQ");
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(connectionString!), h =>
                    {
                        h.Username(builder.Configuration.GetValue<string>("RabbitMQ:Username"));
                        h.Password(builder.Configuration.GetValue<string>("RabbitMQ:Password"));
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