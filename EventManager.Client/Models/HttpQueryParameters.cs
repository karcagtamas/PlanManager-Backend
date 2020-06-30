using System;
using System.Collections.Generic;
using EventManager.Client.Models.Interfaces;

namespace EventManager.Client.Models
{
    public class HttpQueryParameters : IDictionaryState
    {
        private Dictionary<string, object> _queryParams;

        public HttpQueryParameters() {
            this._queryParams = new Dictionary<string, object>();
        }

        public void Add<T>(string key, T value) {
            if (this._queryParams.ContainsKey(key)) {
                throw new ArgumentException("Key already exists");
            }
            _queryParams[key] = value;
        }

        public T Get<T>(string key) {
            if (!this._queryParams.ContainsKey(key)) {
                throw new ArgumentException("Key does not exist");
            }
            return (T)_queryParams[key];
        }

        public void TryAdd<T>(string key, T value) {
            if (this._queryParams.ContainsKey(key)) {
                return;
            }
            _queryParams[key] = value;
        }

        public T TryGet<T>(string key) {
            if (!this._queryParams.ContainsKey(key)) {
                return default;
            }
            return (T)_queryParams[key];
        }

        public int Count() {
            return this._queryParams.Keys.Count;
        }

        override public string ToString() {
            string val = "";

            foreach (var key in this._queryParams.Keys) {
                val += $"{key}={this._queryParams[key].ToString()}";
            }

            return val;
        }
    }
}