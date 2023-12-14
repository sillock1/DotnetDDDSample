using Ordering.Domain.SeedWork;

namespace Ordering.Domain.Aggregates.Order;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string County { get; private set; }
    public string Country { get; private set; }
    public string PostCode { get; private set; }
    
    public Address(string street, string city, string county, string country, string postCode)
    {
        Street = street;
        City = city;
        County = county;
        Country = country;
        PostCode = postCode;
    }
}