using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;

namespace Ticket.Business.Abstract
{
    public interface ITicketService
    {
        Task<IResult> GetAll(PaginationQuery paginationQuery, int userId);
    }
}
