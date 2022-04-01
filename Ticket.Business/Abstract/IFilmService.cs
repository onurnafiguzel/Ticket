using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Abstract
{
    public interface IFilmService
    {
        Task<IDataResult<IList<Film>>> GetAll();
        Task<IDataResult<Film>> Get(int filmId);
        Task<IResult> Add(Film film);
        Task<IResult> Update(Film film);
        Task<IResult> Delete(int filmId);
    }
}
