using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class MovieSession : IEntity
    {
        public int Id { get; set; }

        public int? MovieId { get; set; }
        public Movie Movie { get; set; }

        public int? TheatherId { get; set; }
        public Theather Theather { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }        

        public ICollection<MovieSessionSeat> MovieSessionSeats { get; set; } = new HashSet<MovieSessionSeat>();
    }
}
