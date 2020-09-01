using System.Collections.Generic;

namespace ManagerAPI.Services.Common.Repository
{
    /// <summary>
    /// Notification Arguments
    /// </summary>
    public class NotificationArguments
    {
        public List<string> UpdateArguments { get; set;}
        public List<string> DeleteArguments { get;set; }
        public List<string> CreateArguments { get; set; }

        /// <summary>
        /// Default Init
        /// </summary>
        public NotificationArguments()
        {
            this.CreateArguments = new List<string>();
            this.DeleteArguments = new List<string>();
            this.CreateArguments = new List<string>();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="createArgs">Create arguments</param>
        /// <param name="updateArgs">Update arguments</param>
        /// <param name="deleteArgs">Delete arguments</param>
        public NotificationArguments(List<string> createArgs, List<string> updateArgs, List<string> deleteArgs)
        {
            this.CreateArguments = createArgs;
            this.UpdateArguments = updateArgs;
            this.DeleteArguments = deleteArgs;
        }
    }
}