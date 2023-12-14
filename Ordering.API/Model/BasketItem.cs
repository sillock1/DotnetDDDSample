using System.ComponentModel.DataAnnotations;

namespace Ordering.API.Model;

public class BasketItem : IValidatableObject
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        if (Quantity <= 0)
        {
            results.Add(new ValidationResult("Invalid number of items added", new []{nameof(Quantity)}));
        }
        return results;
    }
}