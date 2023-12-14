using IntegrationEvents;
using MassTransit;

namespace Catalog.API.IntegrationEvents.EventHandling;

public class OrderConfirmedIntegrationEventHandler(
    CatalogContext catalogContext,
    ILogger<OrderConfirmedIntegrationEventHandler> logger) : IConsumer<OrderConfirmedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<OrderConfirmedIntegrationEvent> context)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", context.MessageId, context);

        foreach (var orderStockItem in context.Message.OrderStockItems)
        {
            var catalogItem = await catalogContext.CatalogItems.FindAsync(orderStockItem.ProductId, context.CancellationToken);

            catalogItem!.RemoveStock(orderStockItem.Units);
        }

        await catalogContext.SaveChangesAsync();
    }
}