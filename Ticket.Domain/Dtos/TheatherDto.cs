using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class TheatherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeatPlan { get; set; }
        public PlaceDto Place { get; set; }

        public ICollection<TheatherPriceDto> Prices { get; set; } = new HashSet<TheatherPriceDto>();
    }
}
