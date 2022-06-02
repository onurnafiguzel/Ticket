using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Theather : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeatPlan { get; set; }

        public ICollection<MovieSession> MovieSessions = new HashSet<MovieSession>();
    }
}
