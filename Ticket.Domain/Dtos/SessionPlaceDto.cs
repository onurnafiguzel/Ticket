using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class SessionPlaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }

        public ICollection<SessionTheatherDto> Theathers { get; set; } = new HashSet<SessionTheatherDto>();
    }
}
