using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Business.Models
{
    public interface IPaginationQuery
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
