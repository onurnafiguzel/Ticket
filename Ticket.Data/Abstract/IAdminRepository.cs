using System.Linq.Expressions;
using Ticket.Application.DataAccess;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Abstract
{
    public interface IAdminRepository : IEntityRepository<Admin>
    {
        public Task<IList<UserDto>> GetAdmins(Expression<Func<UserDto, bool>> filter = null);
    }
}
