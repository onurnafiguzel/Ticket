using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class TheatherSeat : IEntity
    {
        public int Id { get; set; }
        public int TheatherId { get; set; }
        public string Name { get; set; }
    }
}
