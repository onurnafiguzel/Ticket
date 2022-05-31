using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Domain.Dtos
{
    public class MovieWithSimiliars
    {
        public Movie Movie { get; set; }
        public IList<Movie> SimiliarMovies { get; set; }
    }
}
