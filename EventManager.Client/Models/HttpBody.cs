using System.Net.Http;
using EventManager.Client.Services;

namespace EventManager.Client.Models
{
    public class HttpBody<T>
    {
        private IHelperService _helperService { get; }
        public T Body { get; set; }

        public HttpBody(IHelperService helperService) {
            this._helperService = helperService;
        }

        public StringContent GetStringContent() {
            return this._helperService.CreateContent(Body);
        }
    }
}