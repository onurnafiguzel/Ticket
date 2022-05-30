using Ticket.Application.Aspects.Autofac.Caching;
using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.Helpers;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IFilmRepository _repository;

        public MovieManager(IFilmRepository repository)
        {
            _repository = repository;
        }

        //TODO ÖNCELİKLİ : Hatalar için bir middleware yazılacak.
        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(FilmValidator))]
        [CacheRemoveAspect("IFilmService.Get")]
        public async Task<IResult> Add(Movie film)
        {
            film.Slug = UrlExtension.FriendlyUrl(film.Title);
            await _repository.AddAsync(film);
            return new SuccessResult(Messages.FilmAdded);
        }

        public async Task<IResult> Delete(int filmId)
        {
            var entity = await _repository.GetAsync(f => f.Id == filmId);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
                return new SuccessResult(Messages.FilmDeleted);
            }
            return new ErrorResult(Messages.FilmNotFound);
        }

        [CacheAspect]
        public async Task<IDataResult<Movie>> Get(int filmId)
        {
            var entity = await _repository.GetAsync(f => f.Id == filmId);
            if (entity != null)
            {
                return new SuccessDataResult<Movie>(entity);
            }
            return new ErrorDataResult<Movie>(Messages.FilmNotFound);
        }

        [CacheAspect]
        public async Task<IDataResult<Movie>> GetBySlug(string slug)
        {
            var entity = await _repository.GetAsync(f => f.Slug == slug);
            if (entity != null)
            {
                return new SuccessDataResult<Movie>(entity);
            }
            return new ErrorDataResult<Movie>(Messages.FilmNotFound);
        }

        [CacheAspect] //key,value
        public async Task<IDataResult<IList<Movie>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return new SuccessDataResult<IList<Movie>>(entities);
        }

        [ValidationAspect(typeof(FilmValidator))]
        [CacheRemoveAspect("IFilmService.Get")]
        public async Task<IResult> Update(Movie film)
        {
            var entity = await _repository.GetAsync(f => f.Id == film.Id);
            if (entity != null)
            {
                await _repository.UpdateAsync(film);
                return new SuccessResult(Messages.FilmUpdated);
            }
            return new ErrorResult(Messages.FilmNotFound);
        }
    }
}
