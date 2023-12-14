namespace Ordering.Infrastructure.EntityConfigurations;

public class OrderItemTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");
        builder.Property(o => o.Id)
            .UseHiLo("orderitemseq");
        
        builder.Property("_productName").HasColumnName("ProductName");
        builder.Property("_itemPrice").HasColumnName("ItemPrice");
        builder.Property("_units").HasColumnName("Units");
        
        builder.Ignore(b => b.DomainEvents);
    }
}