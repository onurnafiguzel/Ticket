using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class MovieTheatherSeat : IEntity
    {
        public int Id { get; set; }
        public int? SeatId { get; set; }
        public int? TheatherId { get; set; }
        public int? MovieId { get; set; }
        public int? UserId { get; set; }
    }
}
