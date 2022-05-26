using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Name { get; set; }

        public ICollection<MovieGenre> Movies = new HashSet<MovieGenre>();
    }
}
