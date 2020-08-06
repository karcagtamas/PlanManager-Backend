using System;

namespace ManagerAPI.Shared.Helpers
{
    public static class DateHelper
    {
        public static string DateToString(DateTime date)
        {
            return $"{date.Year}-{WriteHelper.LeaderZero(date.Month)}-{WriteHelper.LeaderZero(date.Day)} {WriteHelper.LeaderZero(date.Hour)}:{WriteHelper.LeaderZero(date.Minute)}:{WriteHelper.LeaderZero(date.Second)}";
        }

        public static string DateToString(DateTime? date)
        {
            return date == null ? "N/A" : DateToString((DateTime)date);
        }
        
        public static string DateToMonthString(DateTime date)
        {
            return $"{date:yyyy MMMM}";
        }

        public static string DateToWeekString(DateTime date)
        {
            return $"{DateToDayString(date)} - {DateToDayString(date.AddDays(6))}";
        }

        public static string DateToDayString(DateTime date)
        {
            return $"{date:yyyy MMMM dd}";
        }

        public static string DateToNumberDayString(DateTime date)
        {
            return $"{date:yyyy-MM-dd}";
        }
    }
}