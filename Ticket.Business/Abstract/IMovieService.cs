using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IMovieService
    {
        Task<IResult> GetAll(PaginationQuery paginationQuery);
        Task<IDataResult<Movie>> Get(int filmId);
        Task<IDataResult<MovieDto>> GetBySlug(string slug);
        Task<IResult> Add(Movie film);
        Task<IResult> Update(Movie film);
        Task<IResult> Delete(int filmId);
        Task<IDataResult<IList<Cast>>> GetCastsByMovie(string slug);
        Task<IDataResult<IList<Movie>>> GetSimiliarMovies();
        Task<IResult> GetMoviesBySearch(string search, PaginationQuery paginationQuery);
        Task<IDataResult<IList<SessionPlaceDto>>> GetMovieSessions(string slug, int cityId, DateTime dateTime);
    }
}
