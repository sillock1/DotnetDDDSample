using IntegrationEvents;
using IntegrationEvents.Model;
using MassTransit;
using MediatR;
using Ordering.Domain.Aggregates.Buyer;
using Ordering.Domain.Aggregates.Order;
using Ordering.Domain.Events;

namespace Ordering.API.DomainEventHandlers;

public class ValidateOrCreateBuyerDomainEventHandler : INotificationHandler<OrderConfirmedDomainEvent>
{
    private readonly ILogger _logger;
    private readonly IBuyerRepository _buyerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPublishEndpoint _eventPublisher;
    
    public ValidateOrCreateBuyerDomainEventHandler(ILogger<ValidateOrCreateBuyerDomainEventHandler> logger, IBuyerRepository buyerRepository, IOrderRepository orderRepository, IPublishEndpoint eventPublisher)
    {
        _logger = logger;
        _buyerRepository = buyerRepository;
        _orderRepository = orderRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(OrderConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        var buyer = await _buyerRepository.FindByEmailAddress(notification.BuyerEmail);

        if (buyer == null)
        {
            buyer = new Buyer(notification.BuyerName, notification.BuyerEmail);
            _buyerRepository.Add(buyer);
        }
        else
        {
            _buyerRepository.Update(buyer);   
        }
        notification.Order.SetBuyerId(buyer.Id);
        
        await _eventPublisher.Publish(
            new OrderConfirmedIntegrationEvent(
                notification.Order.Id, 
                notification.Order.OrderItems.Select(orderItem => new OrderStockItem(orderItem.ProductId, orderItem.GetUnits()))), cancellationToken);
    }
}