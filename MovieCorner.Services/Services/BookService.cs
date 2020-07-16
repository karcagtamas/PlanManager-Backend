using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Models.DTOs.MC;
using ManagerAPI.Models.Entities.MC;
using ManagerAPI.Models.Models.MC;
using ManagerAPI.Services.Services.Interfaces;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class BookService : IBookService
    {
        // Actions
        private const string UpdateMyBooksAction = "update my books";
        private const string SetBookStatusAction = "set book status";
        private const string DeleteBookAction = "delete book";
        private const string UpdateBookAction = "update book";
        private const string CreateBookAction = "create book";
        private const string GetMyBooksAction = "get my books";
        private const string GetBookAction = "get book";
        private const string GetBooksAction = "get books";

        // Things
        private const string BookThing = "book";
        private const string UserBookThing = "user-book";
        private const string BookIdThing = "book id";

        // Messages
        private const string UserBookConnectionDoesNotExistMessage = "User Book connection does not exist";
        private const string BookDoesNotExistMessage = "Book does not exist";
        private const string BookIdsDoNotMatchMessage = "Book ids do not match";

        // Injects
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUtilsService _utilsService;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public BookService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _utilsService = utilsService;
            _loggerService = loggerService;
        }

        public void CreateBook(BookModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var book = this._mapper.Map<Book>(model);
            book.CreatorId = user.Id;
            book.LastUpdaterId = user.Id;

            _context.Books.Add(book);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(BookService), CreateBookAction, book.Id);
        }

        public void DeleteBook(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var book = _context.Books.Find(id);
            if (book == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(BookService), BookThing, BookDoesNotExistMessage);
            }

            this._context.Books.Remove(book);
            this._context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(BookService), DeleteBookAction, id);
        }

        public BookDto GetBook(int id)
        {
            var user = this._utilsService.GetCurrentUser();

            var book = _context.Books.Find(id);
            
            if (book == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(BookService), BookThing, BookDoesNotExistMessage);
            }

            this._loggerService.LogInformation(user, nameof(BookService), GetBookAction, id);

            return _mapper.Map<BookDto>(book);
        }

        public List<BookListDto> GetBooks()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = this._context.Books.ToList();

            this._loggerService.LogInformation(user, nameof(BookService), GetBooksAction, list.Select(x => x.Id).ToList());

            return this._mapper.Map<List<BookListDto>>(list);
        }

        public List<MyBookDto> GetMyBooks()
        {
            var user = this._utilsService.GetCurrentUser();

            var list = user.MyBooks.ToList();

            this._loggerService.LogInformation(user, nameof(BookService), GetMyBooksAction, list.Select(x => x.Book.Id).ToList());

            return _mapper.Map<List<MyBookDto>>(list);
        }

        public void UpdateBook(int id, BookModel model)
        {
            var user = this._utilsService.GetCurrentUser();

            var book = _context.Books.Find(id);
            if (book == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(BookService), BookThing, BookDoesNotExistMessage);
            }

            this._mapper.Map(model, book);
            book.LastUpdaterId = user.Id;
            book.LastUpdate = DateTime.Now;

            _context.Books.Update(book);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(BookService), UpdateBookAction, book.Id);
        }

        public void UpdateMyBooks(List<int> ids)
        {
            var user = this._utilsService.GetCurrentUser();

            var currentMappings = _context.UserBookSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.BookId) == -1)
                {
                    _context.UserBookSwitch.Remove(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.BookId == i) == null)
                {
                    var map = new UserBook() { BookId = i, UserId = user.Id, Read = false };
                    _context.UserBookSwitch.Add(map);
                }
            }

            this._loggerService.LogInformation(user, nameof(BookService), UpdateMyBooksAction, ids);
            _context.SaveChanges();
        }

        public void UpdateReadStatus(int id, bool status)
        {
            var user = this._utilsService.GetCurrentUser();

            var userBook = _context.UserBookSwitch.Find(user.Id, id);
            if (userBook == null)
            {
                throw this._loggerService.LogInvalidThings(user, nameof(BookService), UserBookThing, UserBookConnectionDoesNotExistMessage);
            }

            userBook.Read = status;
            userBook.ReadOn = status ? (DateTime?)DateTime.Now : null;
            _context.UserBookSwitch.Update(userBook);
            _context.SaveChanges();

            this._loggerService.LogInformation(user, nameof(BookService), SetBookStatusAction, userBook.Book.Id);
        }
    }
}
