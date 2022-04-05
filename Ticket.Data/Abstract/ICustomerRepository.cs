using Ticket.Application.DataAccess;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        public Task<List<OperationClaim>> GetClaims(Customer customer);
    }
}
