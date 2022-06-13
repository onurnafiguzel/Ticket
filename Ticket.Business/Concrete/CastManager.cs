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
    public class CastManager : ICastService
    {
        private readonly ICastRepository castRepository;
        private readonly IActorRepository actorRepository;
        private IMapper mapper;

        public CastManager(ICastRepository castRepository, IMapper mapper, IActorRepository actorRepository)
        {
            this.castRepository = castRepository;
            this.mapper = mapper;
            this.actorRepository = actorRepository;
        }

        public async Task<IDataResult<CastDto>> Get(int castId)
        {
            var result = await castRepository.GetAsync(c => c.Id == castId);
            if (result != null)
            {
                var entity = await actorRepository.GetAsync(c => c.Id == result.ActorId);
                var castDto = mapper.Map<CastDto>(result);
                castDto.Actor = entity;
                return new SuccessDataResult<CastDto>(castDto);
            }
            return new ErrorDataResult<CastDto>($"{castId} numaralı cast bulunamadı");
        }

        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q)
        {
            Expression<Func<Cast, bool>> filter = c => c.Character.Contains(q);
            var entities = await castRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: q != null ? filter : null);
            if (entities != null)
            {
                var castDtos = new List<CastDto>();
                foreach (var entity in entities)
                {
                    var actor = await actorRepository.GetAsync(a => a.Id == entity.ActorId);
                    var castDto = mapper.Map<CastDto>(entity);
                    castDto.Actor = actor;
                    castDtos.Add(castDto);
                }
                var list = castDtos.AsReadOnly();
                var count = await castRepository.CountAsync(filter: q != null ? filter : null);
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.CustomerNotFound);
        }
    }
}
