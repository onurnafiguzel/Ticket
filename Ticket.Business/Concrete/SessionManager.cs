using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class SessionManager : ISessionService
    {
        private ISessionRepository repository;

        public SessionManager(ISessionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IDataResult<SessionDto>> GetSession(int id)
        {
            var result = await repository.GetSession(id);
            if (result!=null)
            {               
                return new SuccessDataResult<SessionDto>(result);
            }
            return new ErrorDataResult<SessionDto>("olmadı be ustam");
        }

        public Task<IDataResult<IList<SeatDto>>> GetSessionSeats(int id)
        {
            throw new NotImplementedException();
        }
    }
}
