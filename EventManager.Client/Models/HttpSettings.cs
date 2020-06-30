using System;

namespace EventManager.Client.Models
{
    public class HttpSettings
    {
        public string Url { get; set; }
        public HttpQueryParameters QueryParameters { get; set; }
        public HttpPathParameters PathParameters { get; set; }
        public ToasterSettings ToasterSettings { get; set; }

        public HttpSettings(string url) {
            this.SetUrl(url);
            this.QueryParameters = new HttpQueryParameters();
            this.PathParameters = new HttpPathParameters();
            this.ToasterSettings = new ToasterSettings();
        }

        public HttpSettings(string url, HttpQueryParameters queryParameters, HttpPathParameters pathParameters) {
            this.SetUrl(url);
            this.QueryParameters = queryParameters == null ? new HttpQueryParameters() : queryParameters;
            this.PathParameters = pathParameters == null ? new HttpPathParameters() : pathParameters;
            this.ToasterSettings = new ToasterSettings();
        }

        public HttpSettings(string url, HttpQueryParameters queryParameters, HttpPathParameters pathParameters, ToasterSettings toasterSettings) {
            this.SetUrl(url);
            this.QueryParameters = queryParameters == null ? new HttpQueryParameters() : queryParameters;
            this.PathParameters = pathParameters == null ? new HttpPathParameters() : pathParameters;
            this.ToasterSettings = toasterSettings == null ? new ToasterSettings() : toasterSettings;
        }

        public HttpSettings(string url, HttpQueryParameters queryParameters, HttpPathParameters pathParameters, string toasterCaption) {
            this.SetUrl(url);
            this.QueryParameters = queryParameters == null ? new HttpQueryParameters() : queryParameters;
            this.PathParameters = pathParameters == null ? new HttpPathParameters() : pathParameters;
            this.ToasterSettings = String.IsNullOrEmpty(toasterCaption) ? new ToasterSettings() : new ToasterSettings(toasterCaption);
        }

        private void SetUrl(string url) {
            if (String.IsNullOrEmpty(url)) {
                throw new ArgumentException("Invalid url");
            }
            this.Url = url;
        }
    }
}