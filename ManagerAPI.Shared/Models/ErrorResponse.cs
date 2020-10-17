using System;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// Error response model
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorResponse Inner { get; set; }
        public string StackTrace { get; set; }

        /// <summary>
        /// Error response from Exception
        /// </summary>
        /// <param name="e">Exception</param>
        public ErrorResponse(Exception e)
        {
            Message = e.Message;
            if (e.InnerException != null)
            {
                Inner = new ErrorResponse(e.InnerException);
            }

            if (e.StackTrace != null)
            {
                StackTrace = e.StackTrace;
            }
        }
    }
}