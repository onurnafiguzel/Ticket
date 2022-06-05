using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class TheatherSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlaceDto Place { get; set; }
    }
}
