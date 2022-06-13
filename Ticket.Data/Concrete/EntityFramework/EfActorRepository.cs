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
    public class EfActorRepository : EfEntityRepositoryBase<Actor>, IActorRepository
    {
        private TicketContext context;
        public EfActorRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }
    }
}
