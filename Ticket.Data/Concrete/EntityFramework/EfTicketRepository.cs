using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfTicketRepository : EfEntityRepositoryBase<Domain.Entities.Concrete.Ticket>, ITicketRepository
    {
        private TicketContext context;
        public EfTicketRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }
    }
}
