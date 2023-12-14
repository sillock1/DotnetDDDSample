namespace Catalog.API.Apis;

public class CatalogServices(
    CatalogContext context,
    ILogger<CatalogServices> logger)
{
    public CatalogContext Context { get; } = context;
    public ILogger<CatalogServices> Logger { get; } = logger;
}