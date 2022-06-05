using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Enums;

namespace Ticket.Domain.Entities.Concrete
{
    public class TheatherPrice
    {
        public int Id { get; set; }
        public int TheatherId { get; set; }
        public UserType Type { get; set; }
        public int Price { get; set; }
        public Theather Theather { get; set; }
    }
}
