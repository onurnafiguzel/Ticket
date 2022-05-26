using Microsoft.EntityFrameworkCore;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfMovieRepository : EfEntityRepositoryBase<Movie>, IFilmRepository
    {
        public EfMovieRepository(DbContext context) : base(context)
        {
        }
    }
}
