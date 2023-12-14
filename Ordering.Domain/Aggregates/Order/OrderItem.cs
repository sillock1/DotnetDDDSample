using Domain.SeedWork;

namespace Ordering.Domain.Aggregates.Order;

public class OrderItem : Entity
{
    private string _productName = null!;
    private decimal _itemPrice;
    private int _units;
    
    public int ProductId { get; private set; }
    protected OrderItem() { }
    public OrderItem(int productId, string productName, decimal itemPrice, int units)
    {
        if (units <= 0)
            throw new DomainException("Units cannot be less than 0");
        
        ProductId = productId;
        _productName = productName;
        _itemPrice = itemPrice;
        _units = units;
    }

    public void AddUnits(int units)
    {
        if (units <= 0)
            throw new DomainException("Units cannot be less than 0");

        _units += units;
    }

    public int GetUnits()
    {
        return _units;
    }
}