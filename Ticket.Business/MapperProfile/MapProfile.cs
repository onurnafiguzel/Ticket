using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Entities.Concrete;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.MapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, MovieSimpleDto>().ReverseMap();
            CreateMap<Customer, UserDto>().ReverseMap();
            CreateMap<Domain.Entities.Concrete.Ticket, TicketDto>().ForMember(dest => dest.Seats, opt => opt.Ignore()).ReverseMap();
            CreateMap<MovieSession, SessionDto>().ReverseMap();
            CreateMap<Theather, TheatherSimpleDto>().ReverseMap();
            CreateMap<Theather, TheatherDto>().ReverseMap();
            CreateMap<TheatherSeat, SeatDto>().ReverseMap();
            CreateMap<Place, PlaceDto>().ReverseMap();
        }
    }
}
