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
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class ActorManager : IActorService
    {
        private readonly IActorRepository actorRepository;
        private IMapper mapper;

        public ActorManager(IActorRepository actorRepository, IMapper mapper)
        {
            this.actorRepository = actorRepository;
            this.mapper = mapper;
        }

        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q = "")
        {
            Expression<Func<Actor, bool>> filter = a => a.Name.Contains(q);
            var entities = await actorRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: q != null ? filter : null);
            if (entities != null)
            {
                var actors = new List<Actor>();
                foreach (var entity in entities)
                {
                    actors.Add(entity);
                }
                var list = actors.AsReadOnly();
                var count = await actorRepository.CountAsync(filter: q != null ? filter : null);
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.ActorsNotFound);
        }
    }
}
