using System;

namespace ManagerAPI.Shared.DTOs.EM
{
    public class EventActionListDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        public string Message { get; set; }
        
        public string User { get; set; }
    }
}