namespace Ordering.Infrastructure.EntityConfigurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(o => o.Id)
            .UseHiLo("orderseq");
        
        builder.Property("_buyerId")
            .HasColumnName("BuyerId");

        builder.OwnsOne<Address>(o => o.Address);

        builder.HasOne<Buyer>()
            .WithMany()
            .HasForeignKey("_buyerId");
        
        builder.Ignore(b => b.DomainEvents);
    }
}