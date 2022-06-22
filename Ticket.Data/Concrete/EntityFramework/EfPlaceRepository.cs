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
    public class EfPlaceRepository : EfEntityRepositoryBase<Place>, IPlaceRepository
    {
        private TicketContext context;
        public EfPlaceRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext)context;
        }

        public async Task<IList<MovieWithTheathers>> GetSessions(Place place, DateTime dateTime)
        {
            var movieIdsResult = from movieSession in context.MovieSessions
                           join theather in context.Theathers
                           on movieSession.TheatherId equals theather.Id

                           join movie in context.Movies
                           on movieSession.MovieId equals movie.Id

                           where theather.PlaceId == place.Id
                           where movieSession.Date.Year == dateTime.Year && movieSession.Date.Month == dateTime.Month && movieSession.Date.Day == dateTime.Day

                           group movie by new
                           {
                               movie.Id
                           } into _movie

                           select _movie.Key.Id;

            IList<int> movieIds = await movieIdsResult.ToListAsync();
            List<MovieWithTheathers> movies = new();

            foreach (var movieId in movieIds)
            {
                var movie = await context.Movies.Where(r => r.Id == movieId).FirstAsync();
                MovieWithTheathers withTheathers = new MovieWithTheathers
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    BackdropPath = movie.BackdropPath,
                    PosterPath = movie.PosterPath,
                    Slug = movie.Slug,
                };

                var theathersResult = from theather in context.Theathers
                                      join movieSession in context.MovieSessions
                                      on theather.Id equals movieSession.TheatherId
                                      where movieSession.MovieId == movie.Id
                                      where movieSession.Date.Year == dateTime.Year && movieSession.Date.Month == dateTime.Month && movieSession.Date.Day == dateTime.Day
                                      where theather.PlaceId == place.Id

                                      group theather by new { theather.Id } into _theather

                                      select new SessionTheatherDto
                                      {
                                          Id = _theather.Key.Id,
                                          Name = (from theather in context.Theathers where theather.Id == _theather.Key.Id select theather).Single().Name
                                      };

                IList<SessionTheatherDto> theathers = await theathersResult.ToListAsync();

                foreach (var theather in theathers)
                {
                    var sessionsResult = from movieSession in context.MovieSessions
                                         where movieSession.TheatherId == theather.Id
                                         where movieSession.MovieId == movie.Id
                                         where movieSession.Date.Year == dateTime.Year && movieSession.Date.Month == dateTime.Month && movieSession.Date.Day == dateTime.Day
                                         orderby movieSession.Date ascending

                                         select new MovieSessionDto
                                         {
                                             Id = movieSession.Id,
                                             Name = movieSession.Name,
                                             Date = movieSession.Date
                                         };

                    IList<MovieSessionDto> sessions = await sessionsResult.ToListAsync();
                    theather.Sessions = sessions;
                }

                withTheathers.Theathers = theathers;
                movies.Add(withTheathers);
            }

            return movies;
        }
    }
}
