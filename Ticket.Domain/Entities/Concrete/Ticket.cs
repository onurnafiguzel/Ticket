using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Domain.Entities.Concrete
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public string Seats { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Created { get; set; }
    }
}
