namespace EventManager.Frontend.Data.Models
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Content { get; set; }
    }
}