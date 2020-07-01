using System.Net.Http;
using EventManager.Client.Services;
using EventManager.Client.Services.Interfaces;

namespace EventManager.Client.Models {

    /// <summary>
    /// HTTP body
    /// </summary>
    /// <typeparam name="T">Type of the content</typeparam>
    public class HttpBody<T> {
        private IHelperService _helperService { get; }
        private T Body { get; set; }

        /// <summary>
        /// Create body
        /// </summary>
        /// <param name="helperService">Helper Service</param>
        /// <param name="body">Content</param>
        public HttpBody (IHelperService helperService, T body) {
            this._helperService = helperService;
            this.Body = body;
        }

        /// <summary>
        /// Create string content from the body
        /// </summary>
        /// <returns>String content</returns>
        public StringContent GetStringContent () {
            return this._helperService.CreateContent (this.Body);
        }
    }
}