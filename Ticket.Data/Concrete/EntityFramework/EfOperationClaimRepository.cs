using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Application.Entities.Concrete;
using Ticket.Data.Abstract;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfOperationClaimRepository : EfEntityRepositoryBase<OperationClaim>, IOperationClaimRepository
    {
        private TicketContext context;

        public EfOperationClaimRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext?)context;
        }
    }
}
