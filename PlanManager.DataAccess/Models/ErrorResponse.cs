using System;

namespace PlanManager.DataAccess.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public ErrorResponse(Exception e)
        {
            this.Message = e.Message;
        }
    }
}