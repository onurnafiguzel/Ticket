using AutoMapper;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.Helpers;
using Ticket.Business.Models;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly ITicketRepository _repository;
        private readonly ISessionRepository sessionRepository;
        private readonly ITheatherRepository theatherRepository;
        private readonly ITheatherSeatRepository theatherSeatRepository;
        private readonly IFilmRepository filmRepository;
        private readonly IPlaceRepository placeRepository;
        private IMapper mapper;

        public TicketManager (ITicketRepository repository, ISessionRepository sessionRepository, ITheatherRepository theatherRepository, ITheatherSeatRepository theatherSeatRepository, IFilmRepository filmRepository, IPlaceRepository placeRepository, IMapper mapper)
        {
            _repository = repository;
            this.sessionRepository = sessionRepository;
            this.theatherRepository = theatherRepository;
            this.theatherSeatRepository = theatherSeatRepository;
            this.filmRepository = filmRepository;
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        public async Task<IResult> GetAll(PaginationQuery paginationQuery, int userId = 0)
        {
            Expression<Func<Domain.Entities.Concrete.Ticket, bool>> filter = r => r.UserId == userId;
            var _tickets = await _repository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize, filter: userId > 0 ? filter : null);
            if (_tickets == null)
            {
                return new ErrorResult("Not found");
            }

            List<TicketDto> tickets = new();
            foreach (var _ticket in _tickets)
            {
                var ticket = mapper.Map<TicketDto>(_ticket);

                // get session
                var session = await sessionRepository.GetAsync(r => r.Id == _ticket.SessionId);
                ticket.Session = mapper.Map<SessionDto>(session);

                // get movie details
                var movie = await filmRepository.GetAsync(r => r.Id == session.MovieId);
                ticket.Session.Movie = mapper.Map<MovieSimpleDto>(movie);

                // get theather details
                var theather = await theatherRepository.GetAsync(r => r.Id == session.TheatherId);
                ticket.Session.Theather = mapper.Map<TheatherDto>(theather, opt => opt.AfterMap((src, dest) => dest.SeatPlan = null));

                // get place details
                var place = await placeRepository.GetAsync(r => r.Id == theather.PlaceId);
                ticket.Session.Theather.Place = mapper.Map<PlaceDto>(place);

                // get seats ids
                List<int> seatsIds = JsonConvert.DeserializeObject<List<int>>(_ticket.Seats) ?? new List<int>();

                // get seats
                var _seats = await theatherSeatRepository.GetAllAsync(r => seatsIds.Contains(r.Id));

                List<SeatDto> seats = new();
                foreach (var _seat in _seats)
                {
                    var seat = mapper.Map<SeatDto>(_seat);
                    seats.Add(seat);
                }

                ticket.Seats = seats;
                tickets.Add(ticket);
            }

            var list = tickets.AsReadOnly();
            var count = await _repository.CountAsync(userId > 0 ? filter : null);
            return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
        }
    }
}
