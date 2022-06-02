using Ticket.Application.DataAccess;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Dtos;

namespace Ticket.Data.Abstract
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        public Task<List<OperationClaim>> GetClaims(Customer customer);
        public Task<IList<string>> GetRoles(Customer customer);
    }
}
