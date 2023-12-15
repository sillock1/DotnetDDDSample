using Ordering.Infrastructure;

namespace Ordering.API;

public class OrderingContextSeed : IDbSeeder<OrderingContext>
{
    public async Task SeedAsync(OrderingContext context)
    {
        await context.SaveChangesAsync();
    }
}