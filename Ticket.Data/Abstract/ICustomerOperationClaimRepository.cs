using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Dtos;

namespace Ticket.Data.Abstract
{
    public interface ICustomerOperationClaimRepository : IEntityRepository<CustomerOperationClaim>
    {        
    }
}
