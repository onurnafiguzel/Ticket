using Microsoft.EntityFrameworkCore;
using System.Linq;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfMovieRepository : EfEntityRepositoryBase<Movie>, IFilmRepository
    {
        private TicketContext context;

        public EfMovieRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext?)context;
        }

        public async Task<IList<Cast>> GetCastByMovie(Movie movie)
        {
            using (context)
            {
                var result = from movie2 in context.Movies
                             join cast in context.Casts
                             on movie2.Id equals cast.MovieId
                             join actor in context.Actors
                             on cast.ActorId equals actor.Id
                             where cast.MovieId == movie.Id
                             select new Cast { Id = cast.Id, Actor = actor, ActorId = actor.Id, Character = cast.Character, MovieId = movie2.Id };
                return await result.ToListAsync();
            }
        }

        public async Task<IList<GenreDto>> GetGenreByMovie(Movie movie)
        {
            using (context)
            {
                var result = from genre in context.Genres
                             join movieGenre in context.MovieGenres
                             on genre.Id equals movieGenre.GenreId
                             where movieGenre.MovieId == movie.Id
                             select new GenreDto { Id = genre.Id, Name = genre.Name };
                return await result.ToListAsync();
            }
        }

        public async Task<IList<SessionPlaceDto>> GetSessionsByMovie(Movie movie, int cityId, DateTime dateTime)
        {
            using (context)
            {
                var placesResult = from movieSession in context.MovieSessions
                             join theather in context.Theathers
                             on movieSession.TheatherId equals theather.Id

                             join place in context.Places
                             on theather.PlaceId equals place.Id

                             where place.CityId == cityId
                             where movieSession.MovieId == movie.Id
                             where movieSession.Date.Year == dateTime.Year && movieSession.Date.Month == dateTime.Month && movieSession.Date.Day == dateTime.Day

                             group place by new
                             {
                                 place.Id
                             } into _place

                             select new SessionPlaceDto
                             {
                                 Id = _place.Key.Id,
                                 Name = (from place in context.Places where place.Id == _place.Key.Id select place).Single().Name,
                                 CityId = (from place in context.Places where place.Id == _place.Key.Id select place).Single().CityId
                             };

                IList<SessionPlaceDto> places = await placesResult.ToListAsync();

                foreach (var place in places)
                {
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

                    place.Theathers = theathers;
                }

                return places;
            }
        }
    }
}
