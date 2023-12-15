using System.Reflection;
using Ordering.Domain.Aggregates.Order;
using FluentValidation;
using MassTransit;
using Ordering.API;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure;
using Ordering.API.Behaviours;
using Ordering.API.Commands;
using Ordering.API.Validations;
using Ordering.Domain.Aggregates.Buyer;

namespace Microsoft.AspNetCore.Hosting;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<OrderingContext>("OrderingDB", settings => settings.DbContextPooling = false);
        
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddMigration<OrderingContext, OrderingContextSeed>();
        }
        
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });
        
        builder.Services.AddSingleton<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        
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