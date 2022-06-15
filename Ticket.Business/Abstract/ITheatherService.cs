using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface ITheatherService
    {
        public Task<IResult> GetAll(PaginationQuery paginationQuery, string q, int placeId);
        public Task<IDataResult<TheatherDto>> Get(int id);
        public Task<string> GetSeatsById(int id);
    }
}
