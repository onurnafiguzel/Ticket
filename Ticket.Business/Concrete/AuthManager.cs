using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Application.Utilities.Security.JWT;
using Ticket.Business.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ICustomerService _customerService;
        private ITokenHelper _tokenHelper;

        public AuthManager(ITokenHelper tokenHelper, ICustomerService customerService)
        {
            _tokenHelper = tokenHelper;
            _customerService = customerService;
        }

        public Task<IDataResult<AccessToken>> CreateAccessToken(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Customer>> Login(CustomerForLoginDto customerForLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Customer>> Register(CustomerForRegisterDto customerForRegisterDto, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> UserExists(string email)
        {
            throw new NotImplementedException();
        }
    }
}
