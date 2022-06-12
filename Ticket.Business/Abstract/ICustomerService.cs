using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IResult> GetAll(PaginationQuery paginationQuery, string q);
        Task<IDataResult<UserDto>> Get(int customerId);
        Task<IResult> Add(Customer customer);
        Task<IResult> Update(Customer customer);
        Task<IResult> Delete(int customerId);
        Task<List<OperationClaim>> GetClaims(Customer customer);
        Task<Customer> GetByMail(string email);
        Task<IDataResult<Customer>> ChangeCustomerRole(int customerId, int roleId);
    }
}
