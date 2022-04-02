using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.Constants;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class FilmManager : IFilmService
    {
        private readonly IFilmRepository _repository;

        public FilmManager(IFilmRepository repository)
        {
            _repository = repository;
        }

        [ValidationAspect(typeof(FilmValidator))]
        public async Task<IResult> Add(Film film)
        {
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

        public async Task<IDataResult<Film>> Get(int filmId)
        {
            var entity = await _repository.GetAsync(f => f.Id == filmId);
            if (entity != null)
            {
                return new SuccessDataResult<Film>(entity);
            }
            return new ErrorDataResult<Film>(Messages.FilmNotFound);
        }

        public async Task<IDataResult<IList<Film>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return new SuccessDataResult<IList<Film>>(entities);
        }

        [ValidationAspect(typeof(FilmValidator))]
        public async Task<IResult> Update(Film film)
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
