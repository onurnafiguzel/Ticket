using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface IPlaceRepository : IEntityRepository<Place>
    {
        public Task<IList<MovieWithTheathers>> GetSessions(Place place, DateTime dateTime);
    }
}
