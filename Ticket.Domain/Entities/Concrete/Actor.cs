using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Actor : IEntity
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string ProfilePath { get; set; }

        public ICollection<Cast>? Casts { get; set; } = new HashSet<Cast>();
    }
}
