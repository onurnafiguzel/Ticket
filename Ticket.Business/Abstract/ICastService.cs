using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Abstract
{
    public interface ICastService
    {
        public Task<IResult> GetAll(PaginationQuery paginationQuery, string q);
        public Task<IDataResult<CastDto>> Get(int castId);
    }
}
