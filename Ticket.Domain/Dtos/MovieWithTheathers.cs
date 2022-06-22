using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class MovieWithTheathers : MovieSimpleDto
    {
        public ICollection<SessionTheatherDto> Theathers { get; set; } = new HashSet<SessionTheatherDto>();
    }
}
