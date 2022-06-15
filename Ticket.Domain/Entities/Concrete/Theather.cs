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
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        public ICollection<MovieSession> MovieSessions { get; set; }  = new HashSet<MovieSession>();
        public ICollection<TheatherPrice> TheatherPrices { get; set; }  = new HashSet<TheatherPrice>();
    }
}
