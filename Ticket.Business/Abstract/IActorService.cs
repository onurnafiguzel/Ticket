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
    public interface IActorService
    {
        public Task<IResult> GetAll(PaginationQuery paginationQuery, string q);
        public Task<IDataResult<ActorDetailDto>> Get(int actorId);
        public Task<IDataResult<ActorDetailDto>> GetBySlug(string slug);
        public Task<IDataResult<IList<MovieSimpleDto>>> GetMoviesBySlug(string slug);
    }
}
