using Ticket.Application.DataAccess;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface IFilmRepository : IEntityRepository<Movie>
    {
        public Task<IList<Cast>> GetCastByMovie(Movie movie);
        public Task<IList<GenreDto>> GetGenreByMovie(Movie movie);
        public Task<IList<MovieSessionDto>> GetSessionsByMovie(Movie movie);
    }
}
