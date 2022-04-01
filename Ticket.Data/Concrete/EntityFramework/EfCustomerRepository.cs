using Microsoft.EntityFrameworkCore;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfCustomerRepository : EfEntityRepositoryBase<Customer>, ICustomerRepository
    {
        public EfCustomerRepository(DbContext context) : base(context)
        {
        }
    }
}
