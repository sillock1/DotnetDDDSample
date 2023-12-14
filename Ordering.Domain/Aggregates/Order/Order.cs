using Domain.SeedWork;

namespace Ordering.Domain.Aggregates.Order;

public class Order : Entity, IAggregateRoot
{
    public int? GetBuyerId => _buyerId;
    private int? _buyerId;
    
    public Address Address { get; private set; } = null!;

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    protected Order()
    {
        _orderItems = new List<OrderItem>();
    }
    public Order(string userEmail, string userName, Address address) : this()
    {
        Address = address;
        
        var orderStartedDomainEvent = new OrderConfirmedDomainEvent(this, userEmail, userName);
        this.AddDomainEvent(orderStartedDomainEvent);
    }

    public void AddOrderItem(int productId, string productName, decimal itemPrice, int units = 1)
    {
        var orderItem = _orderItems.SingleOrDefault(o => o.ProductId == productId);
        if (orderItem is not null)
            orderItem.AddUnits(units);
        else 
            orderItem = new OrderItem(productId, productName, itemPrice, units);
        
        _orderItems.Add(orderItem);
    }

    public void SetBuyerId(int buyerId)
    {
        _buyerId = buyerId;
    }
}