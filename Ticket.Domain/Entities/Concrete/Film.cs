using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Film : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public decimal Rating { get; set; }
        public string Director { get; set; }
    }
}
