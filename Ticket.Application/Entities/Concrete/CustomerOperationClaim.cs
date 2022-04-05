using Ticket.Domain.Entities.Abstract;

namespace Ticket.Application.Entities.Concrete
{
    public class CustomerOperationClaim : IEntity
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? OperationClaimId { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}
