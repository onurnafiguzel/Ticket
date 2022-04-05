using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;

namespace Ticket.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<IList<Customer>>> GetAll();
        Task<IDataResult<Customer>> Get(int customerId);
        Task<IResult> Add(Customer customer);
        Task<IResult> Update(Customer customer);
        Task<IResult> Delete(int customerId);
        Task<List<OperationClaim>> GetClaims(Customer customer);
        Task<Customer> GetByMail(string email);
    }
}
