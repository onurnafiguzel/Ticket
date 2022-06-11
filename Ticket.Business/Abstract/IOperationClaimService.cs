using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;

namespace Ticket.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<IDataResult<IList<OperationClaim>>> GetAll();
    }
}
