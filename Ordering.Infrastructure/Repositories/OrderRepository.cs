namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderingContext _context;
    public IUnitOfWork UnitOfWork => _context;

    public OrderRepository(OrderingContext context)
    {
        _context = context;
    }

    public Order Add(Order order)
    {
        if(order.Id == default)
            _context.Orders.Add(order);
        return order;
    }

    public async Task<Order?> GetAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            await _context.Entry(order)
                .Collection(i => i.OrderItems).LoadAsync();
        }
        
        return order;
    }
}