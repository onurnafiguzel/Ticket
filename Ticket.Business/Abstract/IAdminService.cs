using Ticket.Application.Utilities.Results;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IAdminService
    {
        Task<IDataResult<IList<Admin>>> GetAll();
        Task<IDataResult<Admin>> Get(int adminId);
        Task<IResult> Add(Admin admin);
        Task<IResult> Update(Admin admin);
        Task<IResult> Delete(int adminId);
    }
}
