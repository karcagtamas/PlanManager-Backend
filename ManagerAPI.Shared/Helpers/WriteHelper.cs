using System.Collections.Generic;

namespace ManagerAPI.Shared.Helpers
{
    public static class WriteHelper
    {
        public static string LeaderZero(int number)
        {
            return number.ToString().PadLeft(2, '0');
        }

        public static string WriteNullableField(object fieldValue)
        {
            return fieldValue == null ? "N/A" : fieldValue.ToString();
        }

        public static string WriteEmptyableField(string val)
        {
            return string.IsNullOrEmpty(val) ? "N/A" : val;
        }

        public static string WriteForint(decimal? fieldValue)
        {
            return fieldValue == null ? "-" : $"{fieldValue} Ft";
        }
        
        public static string WriteList(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return "N/A";
            }
            return string.Join(", ", list);
        }
    }
}