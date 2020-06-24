using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManager.Client.Services
{
    public interface IHelperService
    {
        string ConnectionIsUnreachable();
        void Navigate(string path);
        string DateToString(DateTime date);
        string LeaderZero(int number);
        string WriteNullableField(object fieldValue);
        string WriteForint(decimal? fieldValue);
        StringContent CreateContent(object obj);
        JsonSerializerOptions GetSerializerOptions();
        Task AddToaster(HttpResponseMessage response, string caption);
    }
}