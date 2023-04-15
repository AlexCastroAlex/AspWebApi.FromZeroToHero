using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Api.DTO;
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

        public async Task<IEnumerable<BookReponseFull>> GetBooksAsync(CancellationToken cancellationToken) 
        {
            return await _bookRepository.GetBooks().ProjectTo<BookReponseFull>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<BookReponseFull?> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetBookById(bookId).ProjectTo<BookReponseFull>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> AddBookAsync(BookDTO book, CancellationToken cancellationToken)
        {
            var bookDb = _mapper.Map<Book>(book);
            return await _bookRepository.AddBookAsync(bookDb, cancellationToken);
        }

        public async Task<Book?> EditBookAsync(int bookId,BookDTO book, CancellationToken cancellationToken)
        {
            var bookDb = _mapper.Map<Book>(book);
            return await _bookRepository.EditBookAsync(bookId, bookDb, cancellationToken);
        }

        public async Task<IEnumerable<AuthorResponse>> GetAuthorAsync( CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAuthors().ProjectTo<AuthorResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<AuthorResponse?> GetAuthorByIdAsync(int author, CancellationToken cancellationToken)
        {
            return await _bookRepository.GetAuthorById(author).ProjectTo<AuthorResponse>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int?> AddAuthorAsync(AuthorDto author, CancellationToken cancellationToken)
        {
            var authorDb = _mapper.Map<Author>(author);
            return await _bookRepository.AddAuthorAsync(authorDb, cancellationToken);
        }

        public async Task<Author?> EditAuthorAsync(int authorId, AuthorDto author, CancellationToken cancellationToken)
        {
            var authorDb = _mapper.Map<Author>(author);
            return await _bookRepository.EditAuthorAsync(authorId, authorDb, cancellationToken);
        }


    }
}
