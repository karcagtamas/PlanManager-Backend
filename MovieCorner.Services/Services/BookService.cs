using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ManagerAPI.DataAccess;
using ManagerAPI.Domain.Entities.MC;
using ManagerAPI.Services.Common;
using ManagerAPI.Services.Services;
using ManagerAPI.Services.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;
using MovieCorner.Services.Services.Interfaces;

namespace MovieCorner.Services.Services
{
    public class BookService : Repository<Book>, IBookService
    {
        // Things
        private const string UserBookThing = "user-book";

        // Messages
        private const string UserBookConnectionDoesNotExistMessage = "User Book connection does not exist";

        // Injects
        private readonly DatabaseContext DatabaseContext;

        /// <summary>
        /// Injector Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="mapper">Mapper</param>
        /// <param name="utilsService">Utils Service</param>
        /// <param name="loggerService">Logger Service</param>
        public BookService(DatabaseContext context, IMapper mapper, IUtilsService utilsService, ILoggerService loggerService) : base(context, loggerService, utilsService, mapper, "Book")
        {
            this.DatabaseContext = context;
        }

        public List<MyBookDto> GetMyList()
        {
            var user = this.Utils.GetCurrentUser();

            var list = user.MyBooks.ToList();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("get my"), list.Select(x => x.Book.Id).ToList());

            return this.Mapper.Map<List<MyBookDto>>(list);
        }

        public void UpdateMyBooks(List<int> ids)
        {
            var user = this.Utils.GetCurrentUser();

            var currentMappings = DatabaseContext.UserBookSwitch.Where(x => x.UserId == user.Id).ToList();
            foreach (var i in currentMappings)
            {
                if (ids.FindIndex(x => x == i.BookId) == -1)
                {
                    DatabaseContext.UserBookSwitch.Remove(i);
                }
            }

            foreach (var i in ids)
            {
                if (currentMappings.FirstOrDefault(x => x.BookId == i) == null)
                {
                    var map = new UserBook() { BookId = i, UserId = user.Id, Read = false };
                    DatabaseContext.UserBookSwitch.Add(map);
                }
            }

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("update my"), ids);
            DatabaseContext.SaveChanges();
        }

        public void UpdateReadStatus(int id, bool status)
        {
            var user = this.Utils.GetCurrentUser();

            var userBook = DatabaseContext.UserBookSwitch.Find(user.Id, id);
            if (userBook == null)
            {
                throw this.Logger.LogInvalidThings(user, this.GetService(), UserBookThing, UserBookConnectionDoesNotExistMessage);
            }

            userBook.Read = status;
            userBook.ReadOn = status ? (DateTime?)DateTime.Now : null;
            DatabaseContext.UserBookSwitch.Update(userBook);
            DatabaseContext.SaveChanges();

            this.Logger.LogInformation(user, this.GetService(), this.GetEvent("set read status for"), userBook.Book.Id);
        }
    }
}
