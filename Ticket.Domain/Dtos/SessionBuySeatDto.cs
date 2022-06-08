using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Enums;

namespace Ticket.Domain.Dtos
{
    public class SessionBuySeatDto
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
    }
}
