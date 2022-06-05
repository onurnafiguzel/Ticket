using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface ICityRepository : IEntityRepository<City>
    {
    }
}
