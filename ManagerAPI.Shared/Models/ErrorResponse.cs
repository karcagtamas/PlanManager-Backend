using System;

namespace ManagerAPI.Shared.Models {
    public class ErrorResponse {
        public string Message { get; set; }
        public ErrorResponse Inner { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponse (Exception e) {
            this.Message = e.Message;
            if (e.InnerException != null) {
                this.Inner = new ErrorResponse (e.InnerException);
            }
            if (e.StackTrace != null) {
                this.StackTrace = e.StackTrace;
            }
        }
    }
}