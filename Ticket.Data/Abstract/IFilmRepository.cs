using Ticket.Application.DataAccess;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface IFilmRepository : IEntityRepository<Movie>
    {
    }
}
