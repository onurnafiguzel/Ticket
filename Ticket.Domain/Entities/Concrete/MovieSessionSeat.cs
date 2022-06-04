using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class MovieSessionSeat : IEntity
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SeatId { get; set; }
        public int UserId { get; set; }

        public TheatherSeat TheatherSeat { get; set; }
        public MovieSession MovieSession { get; set; }
    }
}
