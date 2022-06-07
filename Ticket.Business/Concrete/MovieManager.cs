using AutoMapper;
using Ticket.Application.Aspects.Autofac.Caching;
using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.Helpers;
using Ticket.Business.Models;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IFilmRepository _repository;
        private IMapper mapper;

        public MovieManager(IFilmRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
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
                //entity.Genres = await _repository.GetGenreByMovie(entity);
                return new SuccessDataResult<Movie>(entity);
            }
            return new ErrorDataResult<Movie>(Messages.FilmNotFound);
        }

        public async Task<IDataResult<MovieDto>> GetBySlug(string slug)
        {
            var entity = await _repository.GetAsync(f => f.Slug == slug);
            if (entity != null)
            {
                var entityDto = mapper.Map<MovieDto>(entity);
                entityDto.Genres = await _repository.GetGenresByMovieId(entity.Id);
                return new SuccessDataResult<MovieDto>(entityDto);
            }
            return new ErrorDataResult<MovieDto>(Messages.FilmNotFound);
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

        // TODO: CacheAspect paginationQuery objesinin değerlerine göre çalışmalıdır, şuan için obje ne olursa olsun sürekli cache hit oluyor.
        // [CacheAspect] //key,value
        public async Task<IResult> GetAll(PaginationQuery paginationQuery)
        {
            var data = await _repository.GetAllPaged(paginationQuery.PageNumber, paginationQuery.PageSize);
            if (data == null)
            {
                return new ErrorDataResult<IList<Movie>>();
            }

            foreach (var movie in data)
            {
                movie.Genres = await _repository.GetGenresByMovieId(movie.Id);
            }

            var count = await _repository.CountAsync();
            return PaginationExtensions.CreatePaginationResult(data, true, paginationQuery, count);
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

        public async Task<IDataResult<IList<SessionPlaceDto>>> GetMovieSessions(string slug, int cityId, DateTime dateTime)
        {
            var movie = await _repository.GetAsync(f => f.Slug == slug);
            if (movie == null)
            {
                return new ErrorDataResult<IList<SessionPlaceDto>>("Böyle bir film bulunamadı");
            }

            var places = await _repository.GetSessionsByMovie(movie, cityId, dateTime);
            if (places.Count == 0)
            {
                return new ErrorDataResult<IList<SessionPlaceDto>>("Bu filme ait session bulunamadı");
            }

            return new SuccessDataResult<IList<SessionPlaceDto>>(places);
        }
    }
}
