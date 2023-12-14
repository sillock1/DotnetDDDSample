namespace Ordering.API.Model;

public record OrderItemDTO
{
    public int ProductId { get; init; }
    public required string ProductName { get; init; }
    public decimal Price { get; init; }
    public int Units { get; init; }
}