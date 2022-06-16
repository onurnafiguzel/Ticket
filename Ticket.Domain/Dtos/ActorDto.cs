using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class ActorDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string ProfilePath { get; set; }
        public string Slug { get; set; }
    }
}
