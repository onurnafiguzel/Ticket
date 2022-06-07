using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Application.Utilities.Results
{
    public interface IPaginationResult<out T>
    {
        IReadOnlyList<T> Data { get; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        int TotalPages { get; }
        int TotalRecords { get; }
    }
}
