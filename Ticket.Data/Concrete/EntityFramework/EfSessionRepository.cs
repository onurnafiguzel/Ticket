using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

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
                var session = from movieSession in context.MovieSessions
                             join theather in context.Theathers
                             on movieSession.TheatherId equals theather.Id
                             join place in context.Places
                             on theather.PlaceId equals place.Id
                             join movie in context.Movies
                             on movieSession.MovieId equals movie.Id
                             where movieSession.Id == id

                             select new SessionDto
                             {
                                 Id = movieSession.Id,
                                 Name = movieSession.Name,
                                 Date = movieSession.Date,
                                 Movie = new MovieSimpleDto
                                 {
                                     Id = movie.Id,
                                     Title = movie.Title,
                                     PosterPath = movie.PosterPath,
                                     Slug = movie.Slug
                                 },
                                 Theather = new TheatherDto
                                 {
                                     Id = theather.Id,
                                     Name = theather.Name,
                                     SeatPlan = theather.SeatPlan,
                                     Place = new PlaceDto
                                     {
                                         Id = place.Id,
                                         Name = place.Name,
                                         CityId = place.CityId,
                                     }
                                 }
                             };

                SessionDto sessionDto = await session.FirstOrDefaultAsync();

                if (sessionDto == null)
                {
                    return sessionDto;
                }

                var theatherPrices = from movieSession in context.MovieSessions
                                     join theatherPrice in context.TheatherPrices
                                     on movieSession.TheatherId equals theatherPrice.TheatherId
                                     where movieSession.Id == id
                                     select new TheatherPriceDto
                                     {
                                         Id = theatherPrice.Id,
                                         Type = theatherPrice.Type,
                                         Price = theatherPrice.Price,
                                     };

                IList<TheatherPriceDto> prices = await theatherPrices.ToListAsync();
                sessionDto.Theather.Prices = prices;

                var seats = from movieSession in context.MovieSessions
                              join theatherSeats in context.TheatherSeats
                              on movieSession.TheatherId equals theatherSeats.TheatherId
                              where movieSession.Id == id

                              select new SeatDto
                              {
                                  Id = theatherSeats.Id,
                                  Name = theatherSeats.Name,
                                  Available = (from movieTheatherSeats in context.MovieSessionSeats where movieTheatherSeats.SeatId == theatherSeats.Id select true).Single() ? false : true
                              };

                IList<SeatDto> seatDtos = await seats.ToListAsync();
                sessionDto.Seats = seatDtos;
                return sessionDto;
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
                                 Available = (from movieTheatherSeats in context.MovieSessionSeats where movieTheatherSeats.SeatId == theatherSeats.Id select true).Single() ? false : true
                             };

                return await result.ToListAsync();
            }
        }
    }
}
