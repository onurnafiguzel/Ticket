using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.Helpers;
using Ticket.Business.Models;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class TheatherManager : ITheatherService
    {
        public readonly ITheatherRepository theatherRepository;
        public IMapper mapper;

        public TheatherManager(ITheatherRepository theatherRepository, IMapper mapper)
        {
            this.theatherRepository = theatherRepository;
            this.mapper = mapper;
        }

        public async Task<IDataResult<Theather>> Get(int id)
        {
            var result = await theatherRepository.GetAsync(t => t.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Theather>(result);
            }
            return new ErrorDataResult<Theather>($"{id} numaralı Theather bulunamadı.");
        }

        [SecuredOperation("god,admin")]
        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q = "", int placeId = 0)
        {
            Expression<Func<Theather, bool>> filter = null;
            Expression<Func<Theather, bool>> filterName = c => c.Name.Contains(q);

            if (q != null && q.Length > 0 && placeId > 0)
            {
                filter = c => c.PlaceId == placeId && c.Name.Contains(q);
            }
            else if (placeId > 0)
            {
                filter = c => c.PlaceId == placeId;
            }
            else if (q != null && q.Length > 0)
            {
                filter = c => c.Name.Contains(q);
            }

            var result = await theatherRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: filter != null ? filter : null);
            if (result != null)
            {
                var placeDto = mapper.Map<List<Theather>>(result);
                var list = placeDto.AsReadOnly();
                var count = await theatherRepository.CountAsync(filter: filter != null ? filter : null);
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.TheatherNotFound);
        }
    }
}
