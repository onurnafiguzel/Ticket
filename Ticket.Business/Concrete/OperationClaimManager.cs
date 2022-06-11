using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Aspects.Autofac.Caching;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Data.Abstract;

namespace Ticket.Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        public IOperationClaimRepository operationClaimRepository;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
        {
            this.operationClaimRepository = operationClaimRepository;
        }

        [SecuredOperation("admin,god")]
        [CacheAspect]
        public async Task<IDataResult<IList<OperationClaim>>> GetAll()
        {
            var result = await operationClaimRepository.GetAllAsync();
            if (result.Count > 0)
            {
                return new SuccessDataResult<IList<OperationClaim>>(result,"Tüm roller");
            }
            return new ErrorDataResult<IList<OperationClaim>>("OperationClaim bulunamadı.");
        }
    }
}
