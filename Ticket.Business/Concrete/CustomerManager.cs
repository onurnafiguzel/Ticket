using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.Constants;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerManager(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public async Task<IResult> Add(Customer customer)
        {
            await _repository.AddAsync(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }

        public async Task<IResult> Delete(int customerId)
        {
            var entity = await _repository.GetAsync(c => c.Id == customerId);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
                return new SuccessResult(Messages.CustomerDeleted);
            }
            return new ErrorResult(Messages.CustomerNotFound);
        }

        public async Task<IDataResult<Customer>> Get(int customerId)
        {
            var entity = await _repository.GetAsync(c => c.Id == customerId);
            if (entity != null)
            {
                return new SuccessDataResult<Customer>(entity);
            }
            return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
        }

        public async Task<IDataResult<IList<Customer>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            if (entities != null)
            {
                return new SuccessDataResult<IList<Customer>>(entities);
            }
            return new ErrorDataResult<IList<Customer>>(Messages.CustomerNotFound);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public async Task<IResult> Update(Customer customer)
        {
            var entity = await _repository.GetAsync(c => c.Id == customer.Id);
            if (entity != null)
            {
                await _repository.UpdateAsync(customer);
                return new SuccessResult(Messages.CustomerUpdated);
            }
            return new ErrorResult(Messages.CustomerNotFound);
        }
    }
}
