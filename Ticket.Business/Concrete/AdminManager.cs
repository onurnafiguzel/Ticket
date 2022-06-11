using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    [SecuredOperation("god")]
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository _repository;

        public AdminManager(IAdminRepository repository)
        {
            _repository = repository;
        }

        [ValidationAspect(typeof(AdminValidator))]
        public async Task<IResult> Add(Admin admin)
        {
            await _repository.AddAsync(admin);
            return new SuccessResult(Messages.AdminAdded);
        }

        public async Task<IResult> Delete(int adminId)
        {
            var entity = await _repository.GetAsync(a => a.Id == adminId);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
                return new SuccessResult(Messages.AdminDeleted);
            }

            return new ErrorResult(Messages.AdminNotFound);
        }

        public async Task<IDataResult<Admin>> Get(int adminId)
        {
            var entity = await _repository.GetAsync(a => a.Id == adminId);
            return new SuccessDataResult<Admin>(entity);
        }

        public async Task<IDataResult<IList<Admin>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return new SuccessDataResult<IList<Admin>>(entities);
        }

        [ValidationAspect(typeof(AdminValidator))]
        public async Task<IResult> Update(Admin admin)
        {
            var entity = await _repository.GetAsync(a => a.Id == admin.Id);
            if (entity != null)
            {
                await _repository.UpdateAsync(admin);
                return new SuccessResult(Messages.AdminUpdated);
            }
            return new ErrorResult(Messages.AdminNotFound);
        }
    }
}
