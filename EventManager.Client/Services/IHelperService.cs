using System;

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
    }
}