using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
