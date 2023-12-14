namespace Ordering.Infrastructure.Repositories;

public class BuyerRepository : IBuyerRepository
{
    private readonly OrderingContext _context;
    public IUnitOfWork UnitOfWork => _context;
    
    public BuyerRepository(OrderingContext context)
    {
        _context = context;
    }

    public Buyer Add(Buyer buyer)
    {
        if(buyer.Id == default)
            _context.Add(buyer);
        return buyer;
    }

    public Buyer Update(Buyer buyer)
    {
        return _context.Buyers.Update(buyer).Entity;
    }

    public async Task<Buyer?> FindByEmailAddress(string emailAddress)
    {
        return await _context.Buyers.SingleOrDefaultAsync(c => c.Email == emailAddress);
    }
}