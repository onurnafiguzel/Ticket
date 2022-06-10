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
    public interface ISessionRepository : IEntityRepository<MovieSession>
    {
        public Task<SessionDto> GetSession(int id);
        public Task<IList<SeatDto>> GetSessionSeats(int id);
    }
}
