﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Domain.Dtos
{
    public class PlaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
