using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<MovieGenre>> GetGenreByMovie(Movie movie)
        {
            using (context)
            {
                var result = from genre in context.Genres
                             join movieGenre in context.MovieGenres
                             on genre.Id equals movieGenre.GenreId
                             where movieGenre.MovieId == movie.Id
                             select new MovieGenre { Id = movieGenre.Id, Genre = genre, GenreId = genre.Id, MovieId = movie.Id };
                return await result.ToListAsync();
            }
        }

        public async Task<IList<MovieSessionDto>> GetSessionsByMovie(Movie movie)
        {
            using (context)
            {
                var result = from movieSession in context.MovieSessions
                             join theather in context.Theathers
                             on movieSession.MovieId equals movie.Id
                             where movieSession.MovieId == movie.Id
                             select new MovieSessionDto { Id = movieSession.Id, Date = movieSession.Date, Name = movieSession.Name, Theather = theather };
                return await result.ToListAsync();
            }
        }
    }
}
