using Microsoft.EntityFrameworkCore;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Application.Entities.Concrete;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfCustomerRepository : EfEntityRepositoryBase<Customer>, ICustomerRepository
    {
        private TicketContext context;

        public EfCustomerRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext?)context;
        }

        public async Task<List<OperationClaim>> GetClaims(Customer customer)
        {
            using (context)
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.CustomerOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CustomerId == customer.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return await result.ToListAsync();
            }
        }

        public async Task<IList<string>> GetRoles(Customer customer)
        {
            using (context)
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.CustomerOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CustomerId == customer.Id
                             select operationClaim.Name;
                return await result.ToListAsync();
            }
        }

        public async Task<Customer> MakeCustomerAdmin(Customer customer)
        {
            customer.OperationClaims.Add(new CustomerOperationClaim
            {
                CustomerId = customer.Id,
                OperationClaimId = 1
            });
            context.Update(customer);
            return customer;
        }
    }
}
