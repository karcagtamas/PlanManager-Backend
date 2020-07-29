using System.Collections.Generic;

namespace ManagerAPI.Services.Common
{
    public class NotificationArguments
    {
        public List<string> UpdateArguments { get; set;}
        public List<string> DeleteArguments { get;set; }
        public List<string> CreateArguments { get; set; }

        public NotificationArguments()
        {
            this.CreateArguments = new List<string>();
            this.DeleteArguments = new List<string>();
            this.CreateArguments = new List<string>();
        }

        public NotificationArguments(List<string> createArgs, List<string> updateArgs, List<string> deleteArgs)
        {
            this.CreateArguments = createArgs;
            this.UpdateArguments = updateArgs;
            this.DeleteArguments = deleteArgs;
        }
    }
}