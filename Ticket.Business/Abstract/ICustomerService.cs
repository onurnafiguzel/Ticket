using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<IList<Customer>>> GetAll();
        Task<IDataResult<Customer>> Get(int customerId);
        Task<IResult> Add(Customer customer);
        Task<IResult> Update(Customer customer);
        Task<IResult> Delete(int customerId);
    }
}
