using FluentValidation.Results;

namespace Ticket.Application.Extensions
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
