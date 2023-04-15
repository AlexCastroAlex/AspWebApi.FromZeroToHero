using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Api.Responses;
using BookStore.Repository.Models;
using BookStore.Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository , IMapper mapper )
        { 
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookReponseFull>> GetBooks(CancellationToken cancellationToken) 
        {
            return await _bookRepository.GetBooksAsync().ProjectTo<BookReponseFull>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

    }
}
