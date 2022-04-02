using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.ValidationRules.FluentValidation
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        public AdminValidator()
        {
            RuleFor(a => a.Name).NotEmpty().WithMessage("İsim alanı boş bırakılamaz.");
            RuleFor(a => a.Email).EmailAddress().WithMessage("Email adresi boş bırakılamaz.");
        }
    }
}
