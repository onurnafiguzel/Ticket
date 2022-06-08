using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Abstract
{
    public interface ISessionService
    {
        public Task<IDataResult<SessionDto>> GetSession(int id);
        public Task<IDataResult<IList<SeatDto>>> GetSessionSeats(int id);
        public Task<IResult> TryBuy(int sessionId, int userId, SessionBuyDto buyDto);
    }
}
