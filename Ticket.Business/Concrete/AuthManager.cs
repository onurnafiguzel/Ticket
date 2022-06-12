using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Aspects.Autofac.Validation;
using Ticket.Application.Entities.Concrete;
using Ticket.Application.Utilities.Results;
using Ticket.Application.Utilities.Security.Hashing;
using Ticket.Application.Utilities.Security.JWT;
using Ticket.Business.Abstract;
using Ticket.Business.Constants;
using Ticket.Business.ValidationRules.FluentValidation;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ICustomerService _customerService;
        private ITokenHelper _tokenHelper;
        private readonly ICustomerRepository customerRepository;

        public AuthManager(ITokenHelper tokenHelper, ICustomerService customerService, ICustomerRepository customerRepository)
        {
            _tokenHelper = tokenHelper;
            _customerService = customerService;
            this.customerRepository = customerRepository;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(Customer customer)
        {
            var claims = await _customerService.GetClaims(customer);
            var accessToken = _tokenHelper.CreateToken(customer, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);

        }

        public async Task<IDataResult<Customer>> Login(CustomerForLoginDto customerForLoginDto)
        {
            var userToCheck = await _customerService.GetByMail(customerForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(customerForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<Customer>(Messages.PasswordError);
            }

            return new SuccessDataResult<Customer>(userToCheck, Messages.SuccesfullLogin);
        }

        public async Task<IDataResult<Customer>> Register(CustomerForRegisterDto customerForRegisterDto, string password)
        {
            byte[] passwodHash, passwordSalt;
            HashingHelper.CreatePasswordHash(customerForRegisterDto.Password, out passwodHash, out passwordSalt);
            var customer = new Customer
            {
                Email = customerForRegisterDto.Email,
                Name = customerForRegisterDto.Name,
                PasswordHash = passwodHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await _customerService.Add(customer);
            return new SuccessDataResult<Customer>(customer, Messages.UserRegistered);
        }
        
        public async Task<IDataResult<Customer>> Update(CustomerUpdateDto customerUpdateDto, int userId)
        {
            Customer currentCustomer = await customerRepository.GetAsync(c => c.Id == userId);
            byte[] passwodHash, passwordSalt;
            if (customerUpdateDto.Password == null || customerUpdateDto.Password.Length == 0)
            {
                Customer customer2 = new Customer
                {
                    Id = userId,
                    Email = customerUpdateDto.Email,
                    Name = customerUpdateDto.Name,
                    PasswordHash = currentCustomer.PasswordHash,
                    PasswordSalt = currentCustomer.PasswordSalt,
                    Status = true
                };
                await _customerService.Update(customer2);
                return new SuccessDataResult<Customer>(customer2, Messages.UserUpdated);
            }
            HashingHelper.CreatePasswordHash(customerUpdateDto.Password, out passwodHash, out passwordSalt);
            var customer = new Customer
            {
                Id = userId,
                Email = customerUpdateDto.Email,
                Name = customerUpdateDto.Name,
                PasswordHash = passwodHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await _customerService.Update(customer);
            return new SuccessDataResult<Customer>(customer, Messages.UserUpdated);
        }

        public async Task<IResult> UserExists(string email)
        {
            if (await _customerService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }
            return new SuccessResult(Messages.UserExist);
        }
    }
}
