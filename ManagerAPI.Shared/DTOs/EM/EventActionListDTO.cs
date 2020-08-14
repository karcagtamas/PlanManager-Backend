using System;

namespace ManagerAPI.Shared.DTOs.EM
{
    /// <summary>
    /// Event action list DTO
    /// </summary>
    public class EventActionListDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}