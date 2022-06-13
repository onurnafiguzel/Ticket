using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Domain.Dtos
{
    public class CastDto
    {
        public int Id { get; set; }
        public Actor Actor { get; set; }
        public string Character { get; set; }
    }
}
