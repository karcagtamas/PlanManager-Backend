using System.Collections.Generic;
using System.Threading.Tasks;
using EventManager.Client.Http;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.MC;
using ManagerAPI.Shared.Models.MC;

namespace EventManager.Client.Services
{
    public class BookService : HttpCall<BookListDto, BookDto, BookModel>, IBookService
    {
        public BookService(IHttpService httpService) : base(httpService, $"{ApplicationSettings.BaseApiUrl}/book", "Book")
        {
        }

        public async Task<bool> AddBookToMyBooks(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Adding book to My Books");

            var body = new HttpBody<object>(null);

            return await this.Http.Create<object>(settings, body);
        }

        public async Task<List<MyBookListDto>> GetMyList()
        {
            var settings = new HttpSettings($"{this.Url}/my");

            return await this.Http.Get<List<MyBookListDto>>(settings);
        }

        public async Task<MyBookDto> GetMy(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            
            var settings = new HttpSettings($"{this.Url}/my", null, pathParams);

            return await this.Http.Get<MyBookDto>(settings);
        }

        public async Task<bool> RemoveBookFromMyBooks(int id)
        {
            var pathParams = new HttpPathParameters();
            pathParams.Add<int>(id, -1);
            var settings = new HttpSettings($"{this.Url}/map", null, pathParams, "Removing book from My Books");

            return await this.Http.Delete(settings);
        }

        public async Task<List<MyBookSelectorListDto>> GetMySelectorList()
        {
            var settings = new HttpSettings($"{this.Url}/selector", null, null);
            
            return await this.Http.Get<List<MyBookSelectorListDto>>(settings);
        }

        public async Task<bool> UpdateMyBooks(MyBookModel model)
        {
            var settings = new HttpSettings($"{this.Url}/map", null, null, "My Books updating");

            var body = new HttpBody<MyBookModel>(model);

            return await this.Http.Update<MyBookModel>(settings, body);
        }

        public async Task<bool> UpdateReadStatus(int id, BookReadStatusModel model)
        {
            var settings = new HttpSettings($"{this.Url}", null, null, "My Book read status updating");

            var body = new HttpBody<BookReadStatusModel>(model);

            return await this.Http.Update<BookReadStatusModel>(settings, body);
        }
    }
}