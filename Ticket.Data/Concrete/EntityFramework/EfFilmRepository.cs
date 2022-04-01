using Microsoft.EntityFrameworkCore;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfFilmRepository : EfEntityRepositoryBase<Film>, IFilmRepository
    {
        public EfFilmRepository(DbContext context) : base(context)
        {
        }
    }
}
