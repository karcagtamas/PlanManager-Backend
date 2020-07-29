using System.Collections.Generic;

namespace ManagerAPI.Services.Common
{
    public class NotificationArguments
    {
        public List<string> UpdateArguments { get; set;}
        public List<string> DeleteArguments { get;set; }
        public List<string> CreateArguments { get; set; }
    }
}