using IntegrationEvents.Model;

namespace IntegrationEvents;

public record OrderConfirmedIntegrationEvent(int orderId, IEnumerable<OrderStockItem> OrderStockItems);