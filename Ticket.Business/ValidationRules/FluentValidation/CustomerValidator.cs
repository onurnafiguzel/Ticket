using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Düzgün bir email girilmelidir.");
        }
    }
}
