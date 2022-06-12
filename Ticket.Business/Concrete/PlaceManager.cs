using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.Constants;
using Ticket.Business.Helpers;
using Ticket.Business.Models;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class PlaceManager : IPlaceService
    {
        public readonly IPlaceRepository placeRepository;
        public IMapper mapper;

        public PlaceManager(IPlaceRepository placeRepository, IMapper mapper)
        {
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q = "", int cityId = 0)
        {
            Expression<Func<Place, bool>> filter = null;
            Expression<Func<Place, bool>> filterName = c => c.Name.Contains(q);

            if (q != null && q.Length > 0 && cityId > 0)
            {
                filter = c => c.CityId == cityId && c.Name.Contains(q);
            }
            else if (cityId > 0)
            {
                filter = c => c.CityId == cityId;
            }
            else if (q != null && q.Length > 0)
            {
                filter = c => c.Name.Contains(q);
            }

            var result = await placeRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: filter != null ? filter : null);
            if (result != null)
            {
                var placeDto = mapper.Map<List<PlaceDto>>(result);
                var list = placeDto.AsReadOnly();
                var count = await placeRepository.CountAsync(filter: filter != null ? filter : null);
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.PlaceNotFound);
        }
    }
}
