using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.ValidationRules.FluentValidation
{
    public class FilmValidator : AbstractValidator<Film>
    {
        public FilmValidator()
        {
            RuleFor(f => f.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez.");
            RuleFor(f => f.Director).NotEmpty().WithMessage("Yönetmen boş geçilemez.");
            RuleFor(f => f.Rating).NotEmpty().WithMessage("Rating boş geçilemez.");
            RuleFor(f => f.Duration).NotEmpty().WithMessage("Süre boş geçilemez.");
            RuleFor(f => f.Description).NotEmpty().WithMessage("Açıklama boş geçilemez.");
        }
    }
}
