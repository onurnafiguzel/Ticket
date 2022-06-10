using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public SessionDto Session { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Created { get; set; }

        public ICollection<SeatDto> Seats { get; set; } = new HashSet<SeatDto>();
    }
}
