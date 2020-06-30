using System.Net.Http;
using EventManager.Client.Services;

namespace EventManager.Client.Models {
    public class HttpBody<T> {
        private IHelperService _helperService { get; }
        private T Body { get; set; }

        public HttpBody (IHelperService helperService, T body) {
            this._helperService = helperService;
            this.Body = body;
        }

        public StringContent GetStringContent () {
            return this._helperService.CreateContent (Body);
        }
    }
}