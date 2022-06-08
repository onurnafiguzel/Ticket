using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess;

namespace Ticket.Data.Abstract
{
    public interface ITicketRepository : IEntityRepository<Domain.Entities.Concrete.Ticket>
    {
    }
}
