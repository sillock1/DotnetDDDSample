namespace Ordering.Domain.Events;

public record OrderConfirmedDomainEvent(
    Order Order,
    string BuyerEmail,
    string BuyerName) : INotification;