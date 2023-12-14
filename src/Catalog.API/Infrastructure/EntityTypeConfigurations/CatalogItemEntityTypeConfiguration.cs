namespace Catalog.API.Infrastructure.EntityTypeConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");
        
        builder.Property(p => p.Name)
            .HasColumnName("Name");
        
        builder.HasIndex(ci => ci.Name);
    }
}