using Ticket.Application.DataAccess;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
    }
}
