namespace Ordering.Infrastructure.EntityConfigurations;

public class BuyerEntityTypeConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyers");
        builder.Property(o => o.Id)
            .UseHiLo("buyerseq");
        
        builder.Property("Name").HasColumnName("Name");
        builder.Property("Email").HasColumnName("Email");
        
        builder.Ignore(b => b.DomainEvents);
    }
}