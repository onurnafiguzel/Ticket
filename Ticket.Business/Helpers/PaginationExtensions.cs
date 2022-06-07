using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Models;

namespace Ticket.Business.Helpers
{
    public static class PaginationExtensions
    {
        public static PaginationDataResult<T> CreatePaginationResult<T>(IReadOnlyList<T> pagedData, bool success,
            PaginationQuery paginationQuery, int totalRecords)
        {
            var result = new PaginationDataResult<T>(pagedData, success, paginationQuery.PageNumber, paginationQuery.PageSize);
            var totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)paginationQuery.PageSize));
            result.TotalPages = totalPages;
            result.TotalRecords = totalRecords;

            return result;
        }
    }
}
