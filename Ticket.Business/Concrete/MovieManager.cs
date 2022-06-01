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
                entity.Genres = await _repository.GetGenreByMovie(entity);
                return new SuccessDataResult<Movie>(entity);
            }
            return new ErrorDataResult<Movie>(Messages.FilmNotFound);
        }

        public async Task<IDataResult<Movie>> GetBySlug(string slug)
        {
            var entity = await _repository.GetAsync(f => f.Slug == slug);
            if (entity != null)
            {
                entity.Genres = await _repository.GetGenreByMovie(entity);
                return new SuccessDataResult<Movie>(entity);
            }
            return new ErrorDataResult<Movie>(Messages.FilmNotFound);
        }

        public async Task<IDataResult<Movie>> GetCastBySlug(string slug)
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

        public async Task<IDataResult<IList<Cast>>> GetCastsByMovie(string slug)
        {
            var film = await _repository.GetAsync(f => f.Slug == slug);
            var charactersAndActors = await _repository.GetCastByMovie(film);
            if (charactersAndActors != null)
            {
                return new SuccessDataResult<IList<Cast>>(charactersAndActors, Messages.CastsShowed);
            }
            return new ErrorDataResult<IList<Cast>>(Messages.CastsNotFound);
        }

        public async Task<IDataResult<IList<Movie>>> GetSimiliarMovies()
        {
            var result = await _repository.GetAllRandomAsync();
            if (result != null)
            {
                return new SuccessDataResult<IList<Movie>>(result);
            }
            return new ErrorDataResult<IList<Movie>>();
        }

        public async Task<IDataResult<IList<Movie>>> GetMoviesBySearch(string search)
        {
            var result = await _repository.GetAllAsync(m => m.Description.Contains(search) || m.Title.Contains(search) || m.OriginalTitle.Contains(search) || m.ImdbId.Contains(search));
            if (result != null)
            {
                return new SuccessDataResult<IList<Movie>>(result);
            }
            return new ErrorDataResult<IList<Movie>>("Bu search için veri blulnamadı");
        }

        public async Task<IDataResult<IList<MovieSession>>> GetMovieSessions(string slug)
        {
            var film = await _repository.GetAsync(f => f.Slug == slug);
            if (film != null)
            {
                var sessions = await _repository.GetSessionsByMovie(film);
                if (sessions.Count>0)
                {
                    return new SuccessDataResult<IList<MovieSession>>(sessions);
                }
                return new ErrorDataResult<IList<MovieSession>>("Bu filme ait session bulunamadı");
            }
            return new ErrorDataResult<IList<MovieSession>>("Böyle bir film bulunamadı");
        }
    }
}
