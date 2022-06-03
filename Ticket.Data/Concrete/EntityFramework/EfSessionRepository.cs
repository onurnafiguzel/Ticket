using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfSessionRepository : EfEntityRepositoryBase<SessionDto>, ISessionRepository
    {
        private TicketContext context;

        public EfSessionRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext?)context;
        }

        public async Task<SessionDto> GetSession(int id)
        {
            using (context)
            {
                var result = from movieSession in context.MovieSessions
                             join theather in context.Theathers
                             on movieSession.TheatherId equals theather.Id
                             where movieSession.Id == id

                             select new SessionDto
                             {
                                 Id = movieSession.Id,
                                 Name = movieSession.Name,
                                 Theather = theather
                             };

                SessionDto sessionDto = await result.FirstOrDefaultAsync();

                if (sessionDto == null)
                {
                    return sessionDto;
                }
                else
                {
                    var result2 = from movieSession in context.MovieSessions
                                  join theatherSeats in context.TheatherSeats
                                  on movieSession.TheatherId equals theatherSeats.TheatherId
                                  where movieSession.Id == id

                                  select new SeatDto
                                  {
                                      Id = theatherSeats.Id,
                                      Name = theatherSeats.Name,
                                      Available = (from movieTheatherSeats in context.MovieTheatherSeats where movieTheatherSeats.SeatId == theatherSeats.Id select true).Single() ? false : true
                                  };

                    IList<SeatDto> seatDtos = await result2.ToListAsync();
                    sessionDto.Seats = seatDtos;
                    return sessionDto;
                }
            }
        }

        public async Task<IList<SeatDto>> GetSessionSeats(int id)
        {
            using (context)
            {
                var result = from movieSession in context.MovieSessions
                             join theatherSeats in context.TheatherSeats
                             on movieSession.TheatherId equals theatherSeats.TheatherId
                             where movieSession.Id == id

                             select new SeatDto
                             {
                                 Id = theatherSeats.Id,
                                 Name = theatherSeats.Name,
                                 Available = (from movieTheatherSeats in context.MovieTheatherSeats where movieTheatherSeats.SeatId == theatherSeats.Id select true).Single() ? false : true
                             };

                return await result.ToListAsync();
            }
        }
    }
}
