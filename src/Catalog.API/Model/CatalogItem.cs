using System.ComponentModel.DataAnnotations;
using Catalog.API.Infrastructure.Exceptions;

namespace Catalog.API.Model;

public class CatalogItem
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [MaxLength(500)]
    public required string Description { get; set; }
    public decimal Price { get; set; }
    
    public int AvailableStock { get; set; }
    
    public int RemoveStock(int quantityDesired)
    {
        if (AvailableStock == 0)
        {
            throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
        }

        if (quantityDesired <= 0)
        {
            throw new CatalogDomainException($"Item units desired should be greater than zero");
        }

        int removed = Math.Min(quantityDesired, this.AvailableStock);

        this.AvailableStock -= removed;

        return removed;
    }
}