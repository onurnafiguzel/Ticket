using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Application.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
            await _context.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            //return filter==null
            //    ? await _context.Set<TEntity>().ToListAsync()
            //    : await _context.Set<TEntity>().Where(filter).ToListAsync();


            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllRandomAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, GetAllAsync().Result.Count);

            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking().Skip(toSkip).Take(6);

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
