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
    public class ActorManager : IActorService
    {
        private readonly IActorRepository actorRepository;
        private readonly ICastRepository castRepository;
        private readonly IFilmRepository movieRepository;
        private IMapper mapper;

        public ActorManager(IActorRepository actorRepository, ICastRepository castRepository, IFilmRepository movieRepository, IMapper mapper)
        {
            this.actorRepository = actorRepository;
            this.castRepository = castRepository;
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<IDataResult<ActorDetailDto>> Get(int actorId)
        {
            var result = await actorRepository.GetAsync(a => a.Id == actorId);
            if (result != null)
            {
                var actorDto = mapper.Map<ActorDetailDto>(result);
                return new SuccessDataResult<ActorDetailDto>(actorDto);
            }
            return new ErrorDataResult<ActorDetailDto>($"{actorId} numaralı aktör bulunamadı.");
        }

        public async Task<IDataResult<ActorDetailDto>> GetBySlug(string slug)
        {
            var result = await actorRepository.GetAsync(a => a.Slug == slug);
            if (result != null)
            {
                var actorDto = mapper.Map<ActorDetailDto>(result);
                return new SuccessDataResult<ActorDetailDto>(actorDto);
            }
            return new ErrorDataResult<ActorDetailDto>($"{slug} numaralı aktör bulunamadı.");
        }

        [SecuredOperation("god,admin")]
        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q = "")
        {
            Expression<Func<Actor, bool>> filter = a => a.Name.Contains(q);
            var entities = await actorRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: q != null ? filter : null);
            if (entities != null)
            {
                var actors = new List<ActorDto>();
                foreach (var entity in entities)
                {
                    var actorDto = mapper.Map<ActorDto>(entity);
                    actors.Add(actorDto);
                }
                var list = actors.AsReadOnly();
                var count = await actorRepository.CountAsync(filter: q != null ? filter : null);
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.ActorsNotFound);
        }

        public async Task<IDataResult<IList<MovieSimpleDto>>> GetMoviesBySlug(string slug)
        {
            var actor = await actorRepository.GetAsync(r => r.Slug == slug);
            var casts = await castRepository.GetAllAsync(r => r.ActorId == actor.Id);
            List<int> movieIds = casts.Select(x => x.MovieId).ToList();

            var movies = await movieRepository.GetAllAsync(r => movieIds.Contains(r.Id));
            if (movies == null)
            {
                return new ErrorDataResult<IList<MovieSimpleDto>>("No movie found");
            }

            List<MovieSimpleDto> _movies = mapper.Map<List<MovieSimpleDto>>(movies);
            return new SuccessDataResult<IList<MovieSimpleDto>>(_movies);
        }
    }
}
