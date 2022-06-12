using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Application.Entities.Concrete;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfCustomerOperaionClaimRepository : EfEntityRepositoryBase<CustomerOperationClaim>, ICustomerOperationClaimRepository
    {
        private TicketContext context;
        public EfCustomerOperaionClaimRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }

        public async Task<IList<UserDto>> GetAdmins()
        {
            var result = from customer in context.Customers
                         join customerOperationClaim in context.CustomerOperationClaims
                         on customer.Id equals customerOperationClaim.CustomerId
                         where customerOperationClaim.OperationClaimId == 1 || customerOperationClaim.OperationClaimId == 3
                         group customer by new
                         {
                             customer.Id
                         } into _customer

                         select new UserDto
                         {
                             Id = _customer.Key.Id,
                             Name = (from customer in context.Customers where customer.Id == _customer.Key.Id select customer).Single().Name,
                             Email = (from customer in context.Customers where customer.Id == _customer.Key.Id select customer).Single().Email,
                         };
            var admins = await result.ToListAsync();

            foreach (var admin in admins)
            {
                var role = from customerOperationClaim in context.CustomerOperationClaims
                           join operationClaim in context.OperationClaims
                           on customerOperationClaim.OperationClaimId equals operationClaim.Id
                           where customerOperationClaim.CustomerId == admin.Id
                           select operationClaim.Name;
                var roles = await role.ToListAsync();
                admin.Roles = roles;
            }
            return admins;
        }
    }
}
