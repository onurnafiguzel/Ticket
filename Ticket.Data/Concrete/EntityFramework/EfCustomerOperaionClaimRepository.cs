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
    }
}
