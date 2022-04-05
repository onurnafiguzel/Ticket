using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;

namespace Ticket.Application.Entities.Concrete
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CustomerOperationClaim> Customers = new HashSet<CustomerOperationClaim>();
    }
}
