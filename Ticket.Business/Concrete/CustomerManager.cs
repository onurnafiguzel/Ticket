using AutoMapper;
using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ICustomerOperationClaimRepository customerOperationClaimRepository;
        private IMapper mapper;

        public CustomerManager(ICustomerRepository repository, IMapper mapper, ICustomerOperationClaimRepository customerOperationClaimRepository)
        {
            _repository = repository;
            this.mapper = mapper;
            this.customerOperationClaimRepository = customerOperationClaimRepository;
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

        public async Task<IDataResult<UserDto>> Get(int customerId)
        {
            var entity = await _repository.GetAsync(c => c.Id == customerId);
            if (entity != null)
            {
                var entityDto = mapper.Map<UserDto>(entity);
                entityDto.Roles = await _repository.GetRoles(entity);
                return new SuccessDataResult<UserDto>(entityDto);
            }
            return new ErrorDataResult<UserDto>(Messages.CustomerNotFound);
        }

        public async Task<IDataResult<IList<UserDto>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            if (entities != null)
            {
                var users = new List<UserDto>();
                foreach (var entity in entities)
                {
                    var user = mapper.Map<UserDto>(entity);
                    user.Roles = await _repository.GetRoles(entity);
                    users.Add(user);
                }
                return new SuccessDataResult<IList<UserDto>>(users);
            }
            return new ErrorDataResult<IList<UserDto>>(Messages.CustomerNotFound);
        }

        public async Task<Customer> GetByMail(string email)
        {
            return await _repository.GetAsync(c => c.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaims(Customer customer)
        {
            return await _repository.GetClaims(customer);
        }

        [SecuredOperation("God")]
        public async Task<IDataResult<Customer>> ChangeCustomerRole(int customerId, int roleId)
        {
            var customer = await _repository.GetAsync(c => c.Id == customerId);
            if (customer == null)
            {
                return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
            }
            var result = await customerOperationClaimRepository.AddAsync(new CustomerOperationClaim { CustomerId = customer.Id, OperationClaimId = roleId });
            customer.OperationClaims.Add(result);
            return new SuccessDataResult<Customer>(customer);
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
