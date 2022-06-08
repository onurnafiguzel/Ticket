﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;
using Ticket.Domain.Entities.Concrete;

namespace Ticket.Business.Concrete
{
    public class SessionManager : ISessionService
    {
        private ISessionRepository repository;
        private ITheatherRepository theatherRepository;
        private ITheatherPriceRepository theatherPriceRepository;
        private ITheatherSeatRepository theatherSeatRepository;
        private IMovieSessionSeatRepository movieSessionSeatRepository;
        private ITicketRepository ticketRepository;

        public SessionManager(ISessionRepository repository, ITheatherRepository theatherRepository, ITheatherPriceRepository theatherPriceRepository, ITheatherSeatRepository theatherSeatRepository, IMovieSessionSeatRepository movieSessionSeatRepository, ITicketRepository ticketRepository)
        {
            this.repository = repository;
            this.theatherRepository = theatherRepository;
            this.theatherPriceRepository = theatherPriceRepository;
            this.theatherSeatRepository = theatherSeatRepository;
            this.movieSessionSeatRepository = movieSessionSeatRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<IDataResult<SessionDto>> GetSession(int id)
        {
            var result = await repository.GetSession(id);
            if (result!=null)
            {               
                return new SuccessDataResult<SessionDto>(result);
            }
            return new ErrorDataResult<SessionDto>("olmadı be ustam");
        }

        public Task<IDataResult<IList<SeatDto>>> GetSessionSeats(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> TryBuy(int sessionId, int userId, SessionBuyDto buyDto)
        {
            // check session exists
            var session = await repository.GetSession(sessionId);
            if (session == null)
            {
                return new ErrorResult("Session not found");
            }

            // check seat count
            if (buyDto.Seats.Count == 0)
            {
                return new ErrorResult("No seats");
            }

            int theatherId = session.Theather.Id;
            IList<SessionBuySeatDto> seats = buyDto.Seats;
            IList<TheatherPrice> prices = await theatherPriceRepository.GetAllAsync();

            // check if seat is exists and belongs to this session's theather
            foreach (var seat in seats)
            {
                var _seat = await theatherSeatRepository.GetAsync(r => r.Id == seat.Id);
                if (_seat == null || _seat.TheatherId != theatherId)
                {
                    return new ErrorResult("One of the seats is corrupted");
                }
            }

            // check price types
            foreach (var seat in seats)
            {
                if (prices.Where(x => x.Type == seat.Type).Count() == 0)
                {
                    return new ErrorResult("One of the seats is corrupted");
                }
            }

            // check seats are available
            foreach (var seat in seats)
            {
                var _seat = await movieSessionSeatRepository.GetAsync(r => r.Id == seat.Id);
                if (_seat != null)
                {
                    return new ErrorResult("One of the seats is unavailable");
                }
            }

            // from this part if all the checks are passed we can proceed to sell

            // begin transaction
            using var transaction = await repository.BeginTransactionAsync();

            try
            {
                // insert seats
                foreach (var seat in seats)
                {
                    var mss = new MovieSessionSeat
                    {
                        SessionId = sessionId,
                        UserId = userId,
                        SeatId = seat.Id
                    };

                    await movieSessionSeatRepository.AddAsync(mss);
                }

                // reduce seats to calculate total ticket price
                var totalTicketPrice = seats.Aggregate(0, (acc, x) => acc + (prices.Where(p => p.Type == x.Type).First().Price));

                // total service fee
                var totalServiceFee = seats.Count * 3.0f;

                // total price
                var totalPrice = totalTicketPrice + totalServiceFee;

                // select only ids
                IList<int> _seatIds = seats.Select(s => s.Id).ToList();
                // convert to json string
                string seatIds = JsonConvert.SerializeObject(_seatIds);

                // create ticket entity
                var _ticket = new Domain.Entities.Concrete.Ticket
                {
                    SessionId = sessionId,
                    UserId = userId,
                    Seats = seatIds,
                    TotalPrice = totalPrice,
                    Created = new DateTime()
                };

                // insert to database
                var ticket = await ticketRepository.AddAsync(_ticket);

                // save and commit changes
                repository.SaveChanges();
                await transaction.CommitAsync();

                return new SuccessDataResult<Domain.Entities.Concrete.Ticket>(ticket, "Ticket created successfuly");
            }
            catch(Exception e)
            {
                await transaction.RollbackAsync();

                return new ErrorResult("Transaction failed");
            }
        }
    }
}
