﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Domain.Entities.Abstract;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Domain.Dtos
{
    public class SessionDto:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Theather Theather { get; set; }
        public ICollection<SeatDto> Seats { get; set; }
    }
}
