using AutoMapper;
using System.Linq.Expressions;
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

        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(FilmValidator))]
        [CacheRemoveAspect("IFilmService.Get")] // todo burası getall mu olması gerek
        public async Task<IResult> Add(Movie film)
        {
            film.Slug = UrlExtension.FriendlyUrl(film.Title);
            await _repository.AddAsync(film);
            return new SuccessResult(Messages.FilmAdded);
        }

        [SecuredOperation("admin,god")]
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

        [CacheAspect] //key,value
        public async Task<IResult> GetAll(PaginationQuery paginationQuery, string q = "")
        {
            Expression<Func<Movie, bool>> filter = c => c.Title.Contains(q) | c.OriginalTitle.Contains(q);
            var data = await _repository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: q != null ? filter : null);
            if (data == null)
            {
                return new ErrorDataResult<IList<Movie>>();
            }

            List<MovieDto> movies = new List<MovieDto>();
            foreach (var movie in data)
            {
                var entity = mapper.Map<MovieDto>(movie);
                entity.Genres = await _repository.GetGenresByMovieId(movie.Id);
                movies.Add(entity);
            }

            var list = movies.AsReadOnly();
            var count = await _repository.CountAsync(filter: q != null ? filter : null);
            return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
        }

        [SecuredOperation("admin,god")]
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
            var limit = 6;
            var count = await _repository.CountAsync();

            // randomly select ids
            var ids = new List<int>();
            var r = new Random();
            while (ids.Count < limit)
            {
                var rand = r.Next(1, count);
                if (!ids.Contains(rand))
                {
                    ids.Add(rand);
                }

                // prevent infinite loop
                if (ids.Count == count)
                {
                    break;
                }
            }

            var result = await _repository.GetAllAsync(r => ids.Contains(r.Id), limit: limit);
            if (result != null)
            {
                return new SuccessDataResult<IList<Movie>>(result);
            }
            return new ErrorDataResult<IList<Movie>>();
        }

        public async Task<IResult> GetMoviesBySearch(string search, PaginationQuery paginationQuery)
        {
            Expression<Func<Movie, bool>> filter = m => m.Description.Contains(search) || m.Title.Contains(search) || m.OriginalTitle.Contains(search) || m.ImdbId.Contains(search);
            var data = await _repository.GetAllAsync(filter, pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize);
            if (data == null)
            {
                return new ErrorDataResult<IList<Movie>>("Bu search için veri bulunamadı");
            }

            List<MovieDto> movies = new List<MovieDto>();
            foreach (var movie in data)
            {
                var entity = mapper.Map<MovieDto>(movie);
                entity.Genres = await _repository.GetGenresByMovieId(movie.Id);
                movies.Add(entity);
            }

            var list = movies.AsReadOnly();
            var count = await _repository.CountAsync(filter);
            return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
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
