using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.DataAccess.EntityFramework;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Data.Concrete.EntityFramework
{
    public class EfCastRepository : EfEntityRepositoryBase<Cast>, ICastRepository
    {
        private TicketContext context;
        private IList<Cast> resultCasts;

        public EfCastRepository(DbContext context) : base(context)
        {
            this.context = (TicketContext?)context;
        }

        public async Task<IList<Cast>> GetCastByMovie(Movie movie)
        {
            using (context)
            {
                var result = from movie2 in context.Movies
                             join cast in context.Casts
                             on movie2.Id equals cast.MovieId                             
                             join actor in context.Actors
                             on cast.ActorId equals actor.Id
                             where cast.MovieId == movie.Id
                             select new Cast { Id = cast.Id, Actor = actor, ActorId = actor.Id, Character = cast.Character, MovieId = movie2.Id };
                return await result.ToListAsync();

            }
        }
    }
}
