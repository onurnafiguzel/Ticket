using Microsoft.EntityFrameworkCore;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Application.Entities.Concrete;
using Ticket.Data.Abstract;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfCustomerRepository : EfEntityRepositoryBase<Customer>, ICustomerRepository
    {

        public EfCustomerRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<OperationClaim>> GetClaims(Customer customer)
        {
            using (var context = new TicketContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.CustomerOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CustomerId == customer.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return await result.ToListAsync();
            }
        }
    }
}
