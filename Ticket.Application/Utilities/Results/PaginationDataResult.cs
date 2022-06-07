using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Application.Utilities.Results
{
    public class PaginationDataResult<T> : Result, IPaginationResult<T>
    {
        public PaginationDataResult(IReadOnlyList<T> data, bool success, int pageNumber, int pageSize) : base(success)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public PaginationDataResult(IReadOnlyList<T> data, bool success, int pageNumber, int pageSize, string message) : base(success, message)
        {
            this.Data = data;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IReadOnlyList<T> Data { get; }
    }
}
