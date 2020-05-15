using System;

namespace ManagerAPI.Models.Models
{
    public class ServerResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string StatusCode { get; set; }
        public T Content { get; set; }
        
        public ServerResponse(T content, bool isSuccess)
        {
            this.Content = content;
            this.IsSuccess = isSuccess;
            this.Message = "It was success";
            this.StatusCode = "200";
        }
        public ServerResponse(Exception e)
        {
            this.Message = e.Message;
            this.IsSuccess = false;
            this.StatusCode = "400";
        }

        
    }
}