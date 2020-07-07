using System;

namespace ManagerAPI.Models.DTOs.EM
{
    public class EventActionListDto
    {
        public DateTime Date { get; set; }
        
        public string Message { get; set; }
        
        public string User { get; set; }
    }
}