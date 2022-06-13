using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Application.Utilities.Results;
using Ticket.Business.Abstract;
using Ticket.Business.BusinessAspects.Autofac;
using Ticket.Business.Constants;
using Ticket.Business.Helpers;
using Ticket.Business.Models;
using Ticket.Data.Abstract;
using Ticket.Domain.Dtos;

namespace Ticket.Business.Concrete
{
    public class GenreManager : IGenreService
    {
        private readonly IGenreRepository genreRepository;
        private IMapper mapper;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        [SecuredOperation("god,admin")]
        public async Task<IDataResult<GenreDto>> Get(int genreId)
        {
            var result = await genreRepository.GetAsync(g => g.Id == genreId);
            if (result != null)
            {
                var genreDto = mapper.Map<GenreDto>(result);
                return new SuccessDataResult<GenreDto>(genreDto);
            }
            return new ErrorDataResult<GenreDto>($"{genreId} numaralı genre bulunamadı");
        }

        [SecuredOperation("god,admin")]
        public async Task<IResult> GetAll(PaginationQuery paginationQuery)
        {
            var result = await genreRepository.GetAllAsync(pageNumber: paginationQuery.PageNumber, pageSize: paginationQuery.PageSize);
            if (result != null)
            {
                var genreDto = mapper.Map<List<GenreDto>>(result);
                var list = genreDto.AsReadOnly();
                var count = await genreRepository.CountAsync();
                return PaginationExtensions.CreatePaginationResult(list, true, paginationQuery, count);
            }
            return new ErrorResult(Messages.GenreNotFound);
        }
    }
}
