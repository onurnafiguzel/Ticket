using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Application.Utilities.Security.JWT;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<Customer>> Register(CustomerForRegisterDto customerForRegisterDto, string password);
        Task<IDataResult<Customer>> Login(CustomerForLoginDto customerForLoginDto);
        Task<IResult> UserExists(string email);
        Task<IDataResult<AccessToken>> CreateAccessToken(Customer customer);
    }
}
