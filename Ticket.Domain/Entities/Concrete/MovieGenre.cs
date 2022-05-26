using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class MovieGenre : IEntity
    {
        public int Id { get; set; }

        public int? MovieId { get; set; }
        public Movie Movie { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
