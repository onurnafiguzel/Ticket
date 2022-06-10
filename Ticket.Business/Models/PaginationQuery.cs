using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Business.Models
{
    public class PaginationQuery : IPaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationQuery()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 10 ? 10 : pageSize;
        }

        public override string ToString()
        {
            return $"{PageNumber},{PageSize}";
        }
    }
}
