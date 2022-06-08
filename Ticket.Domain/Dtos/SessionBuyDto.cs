using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class SessionBuyDto
    {
        public IList<SessionBuySeatDto> Seats { get; set; } = new List<SessionBuySeatDto>();
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardCvc { get; set; }
    }
}
