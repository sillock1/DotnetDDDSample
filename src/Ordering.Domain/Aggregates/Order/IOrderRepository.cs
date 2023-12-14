
namespace Ordering.Domain.Aggregates.Order;

public interface IOrderRepository : IRepository<Order>
{
    Order Add(Order order);

    Task<Order?> GetAsync(int orderId);
}