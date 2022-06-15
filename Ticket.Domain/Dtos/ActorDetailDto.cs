using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class ActorDetailDto : ActorDto
    {
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string ImdbId { get; set; }
    }
}
