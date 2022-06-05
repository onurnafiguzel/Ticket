using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class CityManager : ICityService
    {
        private ICityRepository repository;

        public CityManager(ICityRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IDataResult<IList<City>>> GetAll()
        {
            var result = await repository.GetAllAsync();
            if (result.Count > 0)
            {
                return new SuccessDataResult<IList<City>>(result);
            }
            return new ErrorDataResult<IList<City>>("Şehir bulunamadı");
        }
    }
}
