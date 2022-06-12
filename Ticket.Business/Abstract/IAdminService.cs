using System.Linq.Expressions;
using Ticket.Application.Utilities.Results;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IAdminService
    {
        Task<IDataResult<IList<UserDto>>> GetAll(string q);
        Task<IDataResult<Admin>> Get(int adminId);
        Task<IResult> Add(Admin admin);
        Task<IResult> Update(Admin admin);
        Task<IResult> Delete(int adminId);
    }
}
