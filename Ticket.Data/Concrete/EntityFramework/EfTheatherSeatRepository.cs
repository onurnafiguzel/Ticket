using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfTheatherSeatRepository : EfEntityRepositoryBase<TheatherSeat>, ITheatherSeatRepository
    {
        private TicketContext context;
        public EfTheatherSeatRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }
    }
}
