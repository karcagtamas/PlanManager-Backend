using System;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace EventManager.Client.Services
{
    public class HelperService : IHelperService
    {
        private readonly NavigationManager _navigationManager;
        private readonly string UnreachableMessage = "Connection is unreachable! Please try again later!";

        public HelperService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
        public string ConnectionIsUnreachable()
        {
            return this.UnreachableMessage;
        }

        public void Navigate(string path)
        {
            _navigationManager.NavigateTo(path);
        }

        public string DateToString(DateTime date)
        {
            return $"{date.Year}-{LeaderZero(date.Month)}-{LeaderZero(date.Day)} {LeaderZero(date.Hour)}:{LeaderZero(date.Minute)}:{LeaderZero(date.Second)}";
        }

        public string LeaderZero(int number)
        {
            return number.ToString().PadLeft(2, '0');
        }

        public string WriteNullableField(object fieldValue)
        {
            return fieldValue == null ? "-" : fieldValue.ToString();
        }

        public string WriteForint(decimal? fieldValue)
        {
            return fieldValue == null? "-" : $"{fieldValue} Ft";
        }

        public StringContent CreateContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}