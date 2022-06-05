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

        public async Task<IList<MovieSessionDto>> GetSessionsByMovie(Movie movie, int cityId, DateTime dateTime)
        {
            using (context)
            {
                var result = from movieSession in context.MovieSessions
                             join theather in context.Theathers
                             on movieSession.TheatherId equals theather.Id

                             join places in context.Places
                             on theather.PlaceId equals places.Id
                             where places.CityId == cityId

                             where movieSession.MovieId == movie.Id
                             orderby movieSession.Date ascending
                             select new MovieSessionDto { Id = movieSession.Id, Date = movieSession.Date, Name = movieSession.Name, Theather = theather };
                return await result.Where(m => m.Date.Year == dateTime.Year && m.Date.Month == dateTime.Month && m.Date.Day == dateTime.Day).ToListAsync();
            }
        }
    }
}
