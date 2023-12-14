using System.Runtime.Serialization;
using MediatR;
using Ordering.API.Model;

namespace Ordering.API.Commands;

[DataContract]
public class CreateOrderCommand : IRequest<bool>
{
    [DataMember]
    public string BuyerEmail { get; private set; }
    
    [DataMember]
    public string BuyerName { get; private set; }
    
    [DataMember]
    public string Street { get; private set; }
    
    [DataMember]
    public string City { get; private set; }
    
    [DataMember]
    public string County { get; private set; }
    
    [DataMember]
    public string Country { get; private set; }
    
    [DataMember]
    public string PostCode { get; private set; }

    [DataMember]
    private readonly List<OrderItemDTO> _orderItems = new();
    
    [DataMember]
    public IEnumerable<OrderItemDTO> OrderItems => _orderItems;
    
    public CreateOrderCommand(List<BasketItem> basketItems, string buyerEmail, string buyerName, string street, string city, string county, string country, string postCode)
    {
        _orderItems = basketItems.Select(b => new OrderItemDTO{ProductId = b.ProductId, ProductName = b.ProductName, Price = b.Price, Units = b.Quantity}).ToList();
        BuyerEmail = buyerEmail;
        BuyerName = buyerName;
        Street = street;
        City = city;
        County = county;
        Country = country;
        PostCode = postCode;
    }
}