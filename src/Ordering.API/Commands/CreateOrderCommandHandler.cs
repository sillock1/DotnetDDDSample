using Ordering.Domain.Aggregates.Order;
using MediatR;

namespace Ordering.API.Commands;

public class CreateOrderCommandHandler(IOrderRepository orderRepository)
    : IRequestHandler<CreateOrderCommand, bool>
{
    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(request.Street, request.City, request.County, request.Country, request.PostCode);
        var order = new Order(request.BuyerEmail, request.BuyerName, address);
        foreach (var item in request.OrderItems)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.Units);
        }

        orderRepository.Add(order);
        await orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return true;
    }
}