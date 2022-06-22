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
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class PlaceManager : IPlaceService
    {
        public readonly IPlaceRepository placeRepository;
        public readonly ITheatherRepository theatherRepository;
        public IMapper mapper;

        public PlaceManager(IPlaceRepository placeRepository, ITheatherRepository theatherRepository, IMapper mapper)
        {
            this.placeRepository = placeRepository;
            this.theatherRepository = theatherRepository;
            this.mapper = mapper;
        }

        public async Task<IDataResult<Place>> Get(int id)
        {
            var result = await placeRepository.GetAsync(p => p.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Place>(result);
            }
            return new ErrorDataResult<Place>("Place bulunamadı.");
        }

        [SecuredOperation("god,admin")]
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

        public async Task<IDataResult<IList<MovieWithTheathers>>> GetSessions(int id, DateTime dateTime)
        {
            var place = await placeRepository.GetAsync(f => f.Id == id);
            if (place == null)
            {
                return new ErrorDataResult<IList<MovieWithTheathers>>("Böyle bir mekan bulunamadı");
            }

            var movies = await placeRepository.GetSessions(place, dateTime);
            if (movies.Count == 0)
            {
                return new ErrorDataResult<IList<MovieWithTheathers>>("Bu mekana ait session bulunamadı");
            }

            return new SuccessDataResult<IList<MovieWithTheathers>>(movies);
        }

        public async Task<IDataResult<IList<TheatherSimpleDto>>> GetTheathers(int id)
        {
            var result = await theatherRepository.GetAllAsync(r => r.PlaceId == id);
            if (result == null)
            {
                return new ErrorDataResult<IList<TheatherSimpleDto>>(Messages.TheatherNotFound);
            }

            var theathers = mapper.Map<List<TheatherSimpleDto>>(result);
            return new SuccessDataResult<IList<TheatherSimpleDto>>(theathers);
        }
    }
}
