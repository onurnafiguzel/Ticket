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
    public interface IPlaceService
    {
        public Task<IResult> GetAll(PaginationQuery paginationQuery, string q, int cityId);
        public Task<IDataResult<Place>> Get(int id);
        public Task<IDataResult<IList<TheatherSimpleDto>>> GetTheathers(int id);
        public Task<IDataResult<IList<MovieWithTheathers>>> GetSessions(int id, DateTime dateTime);
    }
}
