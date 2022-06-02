using Ticket.Application.Utilities.Results;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IMovieService
    {
        Task<IDataResult<IList<Movie>>> GetAll();
        Task<IDataResult<Movie>> Get(int filmId);
        Task<IDataResult<MovieDto>> GetBySlug(string slug);
        Task<IResult> Add(Movie film);
        Task<IResult> Update(Movie film);
        Task<IResult> Delete(int filmId);
        Task<IDataResult<IList<Cast>>> GetCastsByMovie(string slug);
        Task<IDataResult<IList<Movie>>> GetSimiliarMovies();
        Task<IDataResult<IList<Movie>>> GetMoviesBySearch(string search);
        Task<IDataResult<IList<MovieSessionDto>>> GetMovieSessions(string slug);
    }
}
