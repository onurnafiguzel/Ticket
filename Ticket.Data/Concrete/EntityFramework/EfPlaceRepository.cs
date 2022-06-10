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
    public class EfPlaceRepository : EfEntityRepositoryBase<Place>, IPlaceRepository
    {
        private TicketContext context;
        public EfPlaceRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }
    }
}
