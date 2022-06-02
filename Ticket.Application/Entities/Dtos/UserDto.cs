using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;

namespace Ticket.Domain.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }     

        public ICollection<string> Roles { get; set; } = new HashSet<string>();
    }
}
