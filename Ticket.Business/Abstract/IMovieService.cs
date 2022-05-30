using Ticket.Application.Utilities.Results;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IMovieService
    {
        Task<IDataResult<IList<Movie>>> GetAll();
        Task<IDataResult<Movie>> Get(int filmId);
        Task<IDataResult<Movie>> GetBySlug(string slug);
        Task<IResult> Add(Movie film);
        Task<IResult> Update(Movie film);
        Task<IResult> Delete(int filmId);
    }
}
