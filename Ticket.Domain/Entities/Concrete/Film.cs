using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Film : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public double Rating { get; set; }
        public string Director { get; set; }
    }
}
