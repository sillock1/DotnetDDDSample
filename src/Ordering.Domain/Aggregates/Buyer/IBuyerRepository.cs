using System.Threading.Tasks;

namespace Ordering.Domain.Aggregates.Buyer;

public interface IBuyerRepository : IRepository<Buyer>
{
    Buyer Add(Buyer buyer);
    Buyer Update(Buyer buyer);
    Task<Buyer?> FindByEmailAddress(string emailAddress);
}