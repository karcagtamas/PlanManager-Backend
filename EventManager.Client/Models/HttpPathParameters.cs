using System;
using System.Collections.Generic;
using EventManager.Client.Models.Interfaces;

namespace EventManager.Client.Models
{
    public class HttpPathParameters : IListState
    {
        
        private List<object> _pathParams;

        public HttpPathParameters() {
            this._pathParams = new List<object>();
        }

        public void Add<T>(T value, int index)
        {
            if (index == -1) {
                this._pathParams.Add(value);
                return;
            }

            if (index < -1) {
                throw new ArgumentException("Index cannot be negative");
            }

            if (index > this._pathParams.Count) {
                throw new ArgumentException("Index cannot be bigger than the list");
            }

            this._pathParams.Insert(index, value);
        }

        public int Count()
        {
            return this._pathParams.Count;
        }

        public T Get<T>(int index)
        {
            if (index < 0) {
                throw new ArgumentException("Index cannot be negative");
            }

            if (index >= this._pathParams.Count) {
                throw new ArgumentException("Index cannot be larger than the list size");
            }

            return (T)this._pathParams[index];
        }

        public void TryAdd<T>(T value, int index)
        {
            if (index == -1) {
                this._pathParams.Add(value);
                return;
            }

            if (index < -1) {
                return;
            }

            if (index > this._pathParams.Count) {
                return;
            }

            this._pathParams.Insert(index, value);
        }

        public T TryGet<T>(int index)
        {
            if (index < 0) {
                return default;
            }

            if (index >= this._pathParams.Count) {
                return default;
            }

            return (T)this._pathParams[index];
        }

        override public string ToString() {
            string val = "";
            
            foreach (var param in this._pathParams) {
                val += $"/{param.ToString()}";
            }

            return val;
        }
    }
}