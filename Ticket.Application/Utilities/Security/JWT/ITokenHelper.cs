using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;

namespace Ticket.Application.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(Customer customer, List<OperationClaim> operationClaims);
    }
}
