using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManager.Client.Services.Interfaces
{
    public interface IHelperService
    {
        string ConnectionIsUnreachable();
        void Navigate(string path);
        string DateToString(DateTime date);
        string DateToString(DateTime? date);
        string DateToDayString(DateTime date);
        string LeaderZero(int number);
        string WriteNullableField(object fieldValue);
        string WriteEmptyableField(string val);
        string WriteForint(decimal? fieldValue);
        StringContent CreateContent(object obj);
        JsonSerializerOptions GetSerializerOptions();
        Task AddToaster(HttpResponseMessage response, string caption);
        string WriteList(List<string> list);
    }
}